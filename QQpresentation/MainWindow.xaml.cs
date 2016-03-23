using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace QQ_presentation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();                                //主窗口关闭
            //  this.Close();  这行代码也是关闭窗口
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;   //主窗口最小化
        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void Online_Click(object sender, RoutedEventArgs e)
        {
            InitialPic.Source = new BitmapImage(new Uri("emotion/online2.png", UriKind.Relative));
        }

        private void Smile_face_Click(object sender, RoutedEventArgs e)
        {
            InitialPic.Source = new BitmapImage(new Uri("emotion/smile_face2.png", UriKind.Relative));
        }

        private void away_Click(object sender, RoutedEventArgs e)
        {
            InitialPic.Source = new BitmapImage(new Uri("emotion/away2.png", UriKind.Relative));
        }

        private void busy_Click(object sender, RoutedEventArgs e)
        {
            InitialPic.Source = new BitmapImage(new Uri("emotion/busy2.png", UriKind.Relative));
        }

        private void not_disturb_Click(object sender, RoutedEventArgs e)
        {
            InitialPic.Source = new BitmapImage(new Uri("emotion/not_disturb2.png", UriKind.Relative));
        }

        private void offline_Click(object sender, RoutedEventArgs e)
        {
            InitialPic.Source = new BitmapImage(new Uri("emotion/offline2.png", UriKind.Relative));
        }

        private void txtbox1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox txtbox1 = sender as TextBox;
            if (txtbox1 != null)
            {
                txtbox1.Focus();    //由这里引发 GotFocus 事件
                e.Handled = true;   //设置 handled 标记阻止路由事件继续传播
            }
        }
        
        private void TxtBox1_GotFocus(object sender, RoutedEventArgs e)  //实现：文本框获取焦点，全选文本内容
        {
            TextBox txtbox1 = e.Source as TextBox;
            txtbox1.SelectAll();    //在 GotFocus 事件里利用 selectall 全选
            txtbox1.PreviewMouseDown -= new MouseButtonEventHandler(txtbox1_PreviewMouseDown); //实现：当第二次单击的时候，不再是全选文字，而是显示光标
            txtbox1.Background = Brushes.White;

        }

        private void TxtBox1_LostFocus(object sender, RoutedEventArgs e)  //文本框丢失焦点后，取消全选，且背景色还原为透明
        {
            TextBox txtbox1 = sender as TextBox;
            if (txtbox1 != null)
            {
                txtbox1.PreviewMouseDown += new MouseButtonEventHandler(txtbox1_PreviewMouseDown);
            }
            txtbox1.Background = Brushes.Transparent;
        }

        string tempTxt2 = string.Empty;    //定义一个全局变量，用来存储获取焦点之前 TextBox 的值
        private void txtbox2_GotFocus(object sender, RoutedEventArgs e)   //获取焦点执行的事件
        {
            TextBox txtbox2 = sender as TextBox;
            tempTxt2 = txtbox2.Text;
            txtbox2.Text = string.Empty;
            txtbox2.Background =Brushes.White;          //获取焦点后，将文本框的背景色改成白色
            txtbox2.BorderBrush = Brushes.Transparent;
            pic_search.Visibility = Visibility.Hidden;    //获取焦点后，隐藏搜索图标
            pic_offline3.Visibility = Visibility.Visible; //获取焦点后，显示关闭图标
        }

        private void txtbox2_LostFocus(object sender, RoutedEventArgs e)   //丢失焦点之后，该处理的事件
        {
            if(txtbox2.Text==string.Empty)
            {
                txtbox2.Text = tempTxt2;
            }
            pic_search.Visibility = Visibility.Visible;      //失去焦点后，重现隐藏图标
            pic_offline3.Visibility = Visibility.Hidden;     //失去焦点后，隐藏关闭图标
        }

        private void Popup_CP_Click(object sender, RoutedEventArgs e)   // 跳转函数：鼠标单击QQ昵称，弹出个人信息的窗口
        {
            MyProfile myprofile = new MyProfile();
            myprofile.ShowDialog();
        }

        private void Window_Loaded(object sender,RoutedEventArgs e)
        {
            Test.GetGroupList().ToList().ForEach(s =>
            {
                Expander t = new Expander();
                t.Header = s;
                t.HeaderTemplate = this.FindResource("ExpanderHeaderTemplate") as DataTemplate;
                ListView v = new ListView();
                v.ItemsSource = s.Children;
                v.Width = 280;
                v.BorderThickness = new Thickness(0);
                v.ItemTemplate = this.FindResource("FriendList") as DataTemplate;
                v.SelectionMode = SelectionMode.Single;
                t.Content = v;
                FriendListControl.Children.Add(t);
                //object obj = this.FindResource("TextBlockStateStyle");
                
            });
        }
    }
}
