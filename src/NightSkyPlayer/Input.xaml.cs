#region Usings
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
#endregion

namespace NightSkyPlayer
{    public partial class Input : Window
    {
        #region Properties
        private NDC.NDynamics.Core.AsyncWorker OpacityWorker = new NDC.NDynamics.Core.AsyncWorker();
        private bool isClosing = false;
        private MainWindow holder;
        #endregion
        #region Ctor
        public Input(MainWindow wn)
        {
            InitializeComponent();
            InitializeControls();

            Opacity = 0;

            holder = wn;
            tb_name.Focus();

            KeyDown += Input_KeyDown;
            OpacityWorker.WorkerInterval += OpacityWorker_WorkerInterval;
            OpacityWorker.RunAsyncWorker(null);
        }
        #endregion
        #region Initializers
        private void InitializeControls()
        {
            lbl_const_name.Override = true;

            tb_name.NLBL_Reference = lbl_name_ph;
            lbl_name_ph.NTB_Reference = tb_name;

            lbl_name_ph.Update();
            tb_name.Update();
            lbl_const_name.Update();

            lbl_const_name.Foreground = new SolidColorBrush(NDC.NStyle.Container.Colors.NIGHTSKY_GLOW);

        }
        #endregion
        #region Events
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    holder.GrabInput(tb_name.Text);
                    isClosing = true;
                    break;
                case Key.Escape:
                    isClosing = true;
                    break;
                default:
                    break;
            }

        }
        private void OpacityWorker_WorkerInterval(object sender, NDC.NDynamics.Arguments.AsyncWorkerArgs e)
        {
            if (Opacity > 1)
                Opacity = 1;
            if (isClosing)
                Opacity -= 0.04;
            else
                Opacity += 0.04;
            if (Opacity <= 0)
            {
                this.Close();
                holder.inputHolder = null;
            }

        }
        #endregion

    }
}
