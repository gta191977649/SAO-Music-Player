using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace SAO_Player
{
    /// <summary>
    /// SpectrumWidget.xaml 的交互逻辑
    /// </summary>
    public partial class SpectrumWidget : SAO_Widget, SAOWidget
    {
        const string author = "Episodes";
        const string verson = "0.0.1 beta";

        private SAOPlayer saoPlayer;
        private DispatcherTimer effectTimer;

        private int Draw_Width = 230;
        private int Draw_Height = 90;
        private int LineWidth = 9;
        private System.Drawing.Color DrawColor1 = System.Drawing.Color.Black;
        private System.Drawing.Color DrawColor2 = System.Drawing.Color.Black;
        private System.Drawing.Color PeakColor = System.Drawing.Color.Black;
        private System.Drawing.Color DrawBackgound = System.Drawing.Color.Transparent;


        public SpectrumWidget(SAOPlayer sPlayer)
        {
            InitializeComponent();
            
            saoPlayer = sPlayer;
            //频谱计时器
            effectTimer = new DispatcherTimer();
            effectTimer.Interval = TimeSpan.FromMilliseconds(1);
            effectTimer.Tick += onEffectDrawn;

            
            if(saoPlayer.isPlay()) onPlay();
        }

        private void initVisual()
        {
            Draw_Width = (int)this.Width;
            Draw_Height = (int)this.Height;
        }
        private void onEffectDrawn(object o, EventArgs e)
        {

            if (saoPlayer.isPlay()) specturm.Source = BitMap.ChangeBitmapToImageSource(saoPlayer.getVisual().CreateSpectrumLinePeak(saoPlayer.getStream(), Draw_Width, Draw_Height, DrawColor1, DrawColor2, PeakColor, DrawBackgound, LineWidth, 3, 1, 30, false, false, true));
            else effectTimer.Stop();
            
            //window.Content = this.Content = "W: " + Draw_Width + "H: " + Draw_Height;
        }


        //观测者推送
        public void onPlay()
        {
            effectTimer.Start();
        }

        public void onStop()
        {
            effectTimer.Stop();
        }

        private void SAO_Widget_Initialized(object sender, EventArgs e)
        {
            initVisual();
        }

        private void SAO_Widget_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            initVisual();
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        private void window_MouseEnter(object sender, MouseEventArgs e)
        {
            //this.Background = Brushes.LightGray;
            this.Background = BC.brushFromHex("#1D000000");
        }

        private void window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.Transparent;
        }
    }
}
