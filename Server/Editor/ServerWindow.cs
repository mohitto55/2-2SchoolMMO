using Server.Debug;
using Server.MySQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerWindow : Form
    {
        public ServerWindow()
        {
            InitializeComponent();

            ListBox_RecivedLog.DrawMode = DrawMode.OwnerDrawVariable;
            ListBox_RecivedLog.MeasureItem += ListBox_RecivedLog_MeasureItem;
            ListBox_RecivedLog.DrawItem += ListBox_RecivedLog_DrawItem;

            ServerDebug.LogEvent += AddItemBoxText;

            ObjectManager.Init();


            PacketHandlerPoolManager.Init();

            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.Init();

            IOCPServer server = new IOCPServer(4826);
            server.Init();
            Task.Run(() => {
                while (server.Run())
                {
                }
            });
            Task.Run(() => {
                while (ObjectManager.Run())
                {
                }
            });
        }

        private void AddItemBoxText(string text)
        {

            ListBox_RecivedLog.SelectedIndex = ListBox_RecivedLog.Items.Count - 1;
            // UI 스레드에서만 ListBox_RecivedLog를 수정할 수 있으므로, Invoke를 사용하여 스레드 안전하게 작업을 처리
            if (ListBox_RecivedLog.InvokeRequired)
            {
                // InvokeRequired가 true이면 UI 스레드에서 작업을 해야 하므로 Invoke를 사용
                ListBox_RecivedLog.Invoke(new Action(() =>
                {
                    ListBox_RecivedLog.Items.Add(text);  // UI 스레드에서 항목 추가
                }));
            }
            else
            {
                // 이미 UI 스레드에서 호출된 경우, 바로 항목을 추가
                ListBox_RecivedLog.Items.Add(text);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void ServerWindow_Load(object sender, EventArgs e)
        {

        }

        private void ListBox_RecivedLog_MeasureItem(object? sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(ListBox_RecivedLog.Items[e.Index].ToString(), ListBox_RecivedLog.Font, ListBox_RecivedLog.Width).Height;
        }

        private void ListBox_RecivedLog_DrawItem(object? sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(ListBox_RecivedLog.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
    }
}
