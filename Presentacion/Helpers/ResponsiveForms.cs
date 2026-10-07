using System.Runtime.CompilerServices;

namespace Nk_Colletion_New.Presentacion.Helpers;

internal sealed class ResponsiveForms : IMessageFilter
{
    private const int WmSize = 0x0005;
    private static readonly ConditionalWeakTable<Control, LayoutSnapshot> Snapshots = new();
    private static readonly List<WeakReference<Control>> RegisteredControls = new();
    private static readonly object SyncRoot = new();
    private bool _isResizing;

    public static void Register(Control control)
    {
        ArgumentNullException.ThrowIfNull(control);
        Snapshots.GetValue(control, root => new LayoutSnapshot(root));
        lock (SyncRoot)
        {
            RegisteredControls.Add(new WeakReference<Control>(control));
        }
    }

    public bool PreFilterMessage(ref Message message)
    {
        if (message.Msg != WmSize || _isResizing)
        {
            return false;
        }

        _isResizing = true;
        try
        {
            var controls = Application.OpenForms.Cast<Control>().ToList();
            lock (SyncRoot)
            {
                for (int index = RegisteredControls.Count - 1; index >= 0; index--)
                {
                    if (RegisteredControls[index].TryGetTarget(out Control? control))
                    {
                        if (!controls.Contains(control)) controls.Add(control);
                    }
                    else
                    {
                        RegisteredControls.RemoveAt(index);
                    }
                }
            }

            foreach (Control control in controls)
            {
                if (control.IsDisposed || !control.IsHandleCreated ||
                    control is Form { WindowState: FormWindowState.Minimized })
                {
                    continue;
                }

                Snapshots.GetValue(control, root => new LayoutSnapshot(root)).Apply();
            }
        }
        finally
        {
            _isResizing = false;
        }

        return false;
    }

    private sealed class LayoutSnapshot
    {
        private readonly Control _root;
        private readonly Size _initialSize;
        private readonly List<ControlSnapshot> _children;

        public LayoutSnapshot(Control root)
        {
            _root = root;
            _initialSize = root.ClientSize;
            _children = root.Controls.Cast<Control>().Select(control => new ControlSnapshot(control)).ToList();
        }

        public void Apply()
        {
            if (_root.ClientSize.Width <= 0 || _root.ClientSize.Height <= 0 ||
                _initialSize.Width <= 0 || _initialSize.Height <= 0)
            {
                return;
            }

            float scaleX = (float)_root.ClientSize.Width / _initialSize.Width;
            float scaleY = (float)_root.ClientSize.Height / _initialSize.Height;
            foreach (ControlSnapshot child in _children)
            {
                if (!child.Control.IsDisposed && child.Control.Parent == _root)
                {
                    child.Apply(scaleX, scaleY);
                }
            }
        }
    }

    private sealed class ControlSnapshot
    {
        private readonly Rectangle _bounds;
        private readonly LayoutSnapshot? _nested;

        public ControlSnapshot(Control control)
        {
            Control = control;
            _bounds = control.Bounds;
            if (control.Controls.Count > 0)
            {
                _nested = new LayoutSnapshot(control);
            }
        }

        public Control Control { get; }

        public void Apply(float scaleX, float scaleY)
        {
            if (Control.Dock == DockStyle.None && Control.Anchor == (AnchorStyles.Top | AnchorStyles.Left))
            {
                Control.Bounds = new Rectangle(
                    (int)Math.Round(_bounds.X * scaleX),
                    (int)Math.Round(_bounds.Y * scaleY),
                    Math.Max(1, (int)Math.Round(_bounds.Width * scaleX)),
                    Math.Max(1, (int)Math.Round(_bounds.Height * scaleY)));
            }

            _nested?.Apply();
        }
    }
}
