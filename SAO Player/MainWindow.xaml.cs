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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Windows.Threading;
using Microsoft.Win32;

namespace SAO_Player
{

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : BaseWindow , Subject
    {
        private List<SAO_Widget> SAOWidgets = new List<SAO_Widget>();

        private Setting settingdialog;
        private SAOPlayer saoPlayer;
        private BitMap bitMap = new BitMap();
        private IntPtr handle;
        private DispatcherTimer effectTimer;

        //Draw With
        private int Draw_Width =230;
        private int Draw_Height = 90;
        private int LineWidth = 3;
        private System.Drawing.Color DrawColor1 = System.Drawing.Color.Black;
        private System.Drawing.Color DrawColor2 = System.Drawing.Color.Black;
        private System.Drawing.Color PeakColor = System.Drawing.Color.Black;
        private System.Drawing.Color DrawBackgound = System.Drawing.Color.Transparent;

        public void attach(SAO_Widget saoWidget)
        {
            SAOWidgets.Add(saoWidget);
        }
        public void detach(SAO_Widget saoWidget)
        {
            SAOWidgets.Remove(saoWidget);
        }

        private void observersOnPlay()
        {
            foreach (SAOWidget o in SAOWidgets)
                o.onPlay();    
        }
        private void observersOnStop()
        {
            foreach (SAOWidget o in SAOWidgets)
                o.onStop();
        }

        private void observersShowAll()
        {
            foreach (SAO_Widget o in SAOWidgets)
                o.ShowDialog();
        }
        public MainWindow()
        {
            InitializeComponent();
           
            
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            init();
            //initWidgets();  
        }

        private void init()
        {
            //获取主窗口句柄
            handle = new WindowInteropHelper(this).Handle;
            
            saoPlayer = new SAOPlayer(handle);
            //频谱计时器
            effectTimer = new DispatcherTimer();
            effectTimer.Interval = TimeSpan.FromMilliseconds(1);
            effectTimer.Tick += onEffectDrawn;

        }
        private void initWidgets()
        {
            
            
        }
        private void onEffectDrawn(object o, EventArgs e)
        {
            
            //if (saoPlayer.isPlay()) specturm.Source = BitMap.CreateBitmapSourceFromBitMap(saoPlayer.getVisual().CreateSpectrumLinePeak(saoPlayer.getStream(), Draw_Width, Draw_Height, DrawColor1, DrawColor2, PeakColor, DrawBackgound, LineWidth, 3, 1, 30, false, false, true));
            if (saoPlayer.isPlay()) specturm.Source = BitMap.ChangeBitmapToImageSource(saoPlayer.getVisual().CreateSpectrumLinePeak(saoPlayer.getStream(), Draw_Width, Draw_Height, DrawColor1, DrawColor2, PeakColor, DrawBackgound, LineWidth, 3, 1, 30, false, false, true));
            else
            {
                effectTimer.Stop();
                observersOnStop();
            }
             
        }

        private void Grid_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

     

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if ((bool)openDialog.ShowDialog())
            {
                lb_title.Content = openDialog.SafeFileName;
                saoPlayer.play(openDialog.FileName);
                effectTimer.Start();
                //推送观察者
                observersOnPlay();
       
            }
            
        }

        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            settingdialog = new Setting();
            settingdialog.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SAO_Widget saoSpecturm = new SpectrumWidget(saoPlayer);
            attach(saoSpecturm);
            saoSpecturm.Show();
            

        }
    }
}
