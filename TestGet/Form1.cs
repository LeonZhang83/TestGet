using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGet
{
    public partial class Form1 : Form
    {

        string finalResult ="";
        string result = "";

        public Form1()
        {
            InitializeComponent();
            Timer time = new Timer();
            time.Enabled = true;
            time.Interval = 3000;
            time.Tick += new EventHandler(timer1_Tick);
            time.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            WebRequest request = WebRequest.Create(@"https://www.playsport.cc/livescore.php?aid=3");
            WebResponse response;
            StreamReader reader;
            request.Method = "GET";
            response = request.GetResponse();
            reader = new StreamReader(response.GetResponseStream());
            result = reader.ReadToEnd();
            reader.Close();
            response.Close();
            finalResult = "";
            getMatchList();
            finalResult = changeENGtoCH(finalResult);
            putInList(finalResult);

            Random random = new Random();
            timer1.Interval = random.Next(5, 20) * 1000 ;
            timer1.Start();
        }

        public void getMatchList()
        {
            string dateTime = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            string compare = "\"NBA_" + dateTime + "_";
            string timeFix1 = "", timeFix2 = "";
            int gameMatch = 0, gameAscore = 0, gameHscore = 0, gameQname = 0, gameTime = 0;
            int A1score = 0, A2score = 0, A3score = 0, A4score = 0, AOTscore = 0;
            int H1score = 0, H2score = 0, H3score = 0, H4score = 0, HOTscore = 0;

            while (gameMatch != -1)
            {
                gameMatch = result.IndexOf(compare);
                if (gameMatch == -1) { break; }
                result = result.Remove(0, gameMatch + 14);
                finalResult += result.Substring(0, 7) + "\n";

                gameAscore = result.IndexOf("_asr_big\">");
                result = result.Remove(0, gameAscore + 10);
                finalResult += result.Substring(0, 3) + " : ";

                gameQname = result.IndexOf("_inning_big\">");
                result = result.Remove(0, gameQname + 13);
                timeFix1 = result.Substring(0, 75).Replace(" ", "");
                timeFix1 = timeFix1.Replace("\n", "");

                gameTime = result.IndexOf("_trm_big\">");
                result = result.Remove(0, gameTime + 10);
                timeFix2 = result.Substring(0, 80).Replace(" ", "");
                timeFix2 = timeFix2.Replace("\n", "");

                gameHscore = result.IndexOf("_hsr_big\">");
                result = result.Remove(0, gameHscore + 10);
                finalResult += result.Substring(0, 3) + "\n";

                finalResult += timeFix1 + " " + timeFix2 + "\n";

                A1score = result.IndexOf("_as1\">");
                result = result.Remove(0, A1score + 6);
                if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "-"; }
                else
                {
                    finalResult += result.Substring(0, 2) + "-";
                }

                A2score = result.IndexOf("_as2\">");
                result = result.Remove(0, A2score + 6);
                if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "-"; }
                else
                {
                    finalResult += result.Substring(0, 2) + "-";
                }

                A3score = result.IndexOf("_as3\">");
                result = result.Remove(0, A3score + 6);
                if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "-"; }
                else
                {
                    finalResult += result.Substring(0, 2) + "-";
                }

                A4score = result.IndexOf("_as4\">");
                result = result.Remove(0, A4score + 6);
                if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "-"; }
                else
                {
                    finalResult += result.Substring(0, 2) + "-";
                }

                AOTscore = result.IndexOf("_as5\">");
                result = result.Remove(0, AOTscore + 6);
                if (result.Substring(0, 2) != "</")
                {
                    if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "\n"; }
                    else
                    {
                        finalResult += result.Substring(0, 2) + "\n";
                    }
                }
                else
                {
                    finalResult = finalResult.Remove(finalResult.Length - 1, 1);
                    finalResult += "\n";
                }

                H1score = result.IndexOf("_hs1\">");
                result = result.Remove(0, H1score + 6);
                if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "-"; }
                else
                {
                    finalResult += result.Substring(0, 2) + "-";
                }

                H2score = result.IndexOf("_hs2\">");
                result = result.Remove(0, H2score + 6);
                if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "-"; }
                else
                {
                    finalResult += result.Substring(0, 2) + "-";
                }

                H3score = result.IndexOf("_hs3\">");
                result = result.Remove(0, H3score + 6);
                if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "-"; }
                else
                {
                    finalResult += result.Substring(0, 2) + "-";
                }

                H4score = result.IndexOf("_hs4\">");
                result = result.Remove(0, H4score + 6);
                if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "-"; }
                else
                {
                    finalResult += result.Substring(0, 2) + "-";
                }

                HOTscore = result.IndexOf("_hs5\">");
                result = result.Remove(0, HOTscore + 6);
                if (result.Substring(0, 2) != "</")
                {
                    if (result.Substring(0, 2).Contains("<")) { finalResult += "  " + result.Substring(0, 1) + "\n"; }
                    else
                    {
                        finalResult += result.Substring(0, 2) + "\n";
                    }
                }
                else
                {
                    finalResult = finalResult.Remove(finalResult.Length - 1,1);
                    finalResult += "\n";
                }
            }
            finalResult = finalResult.Replace("\"", "");
            finalResult = finalResult.Replace("<", "");
        }

        public string changeENGtoCH(string match)
        {
            string[] teamNameE = {"TOR", "MIL", "IND", "BOS", "PHI", "DET", "CHA", "ORL", "BKN", "MIA", "WAS", "CHI", "NY", "ATL", "CLE",
            "GS", "DEN", "POR", "MEM", "LAC", "OKC", "LAL", "NO", "SAC", "SA", "HOU", "UTA", "MIN", "DAL", "PHO"};
            string[] teamNameC = {"暴龍", "公鹿", "溜馬", "塞爾提克", "76人", "活塞", "黃蜂", "魔術", "籃網", "熱火", "巫師", "公牛", "尼克", "老鷹", "騎士",
            "勇士", "金塊", "拓荒者", "灰熊", "快艇", "雷霆", "湖人", "鵜鶘", "國王", "馬刺", "火箭", "爵士", "灰狼", "獨行俠", "太陽"};
            for(int i = 0; i < teamNameC.Length; i++)
            {
                match = match.Replace(teamNameE[i], teamNameC[i]);
            }
            match = match.Replace("@", "(客)對(主)");
            return match;
        }

        public void putInList(string final)
        {
            string listName = final;
            listBox1.Items.Clear();
            int div1 = 0, div2 = 0;
            while (div1 != -1)
            {
                div1 = final.IndexOf("(客)對(主)");
                if(div1 == -1) { break; }
                string Aname = "(客) " + final.Substring(0, div1);
                final=final.Remove(0, div1 + 7);

                div2 = final.IndexOf("\n");
                string Hname = "(主) " + final.Substring(0, div2);
                final=final.Remove(0, div2 + 1);

                div2 = final.IndexOf(" : ");
                string Ascore = final.Substring(0, div2);
                final = final.Remove(0, div2 + 3);

                div2 = final.IndexOf("\n");
                string Hscore = final.Substring(0, div2);
                final = final.Remove(0, div2 + 1);

                string matchScore = "(客) " + Ascore + " : " + Hscore + " (主)";

                div2 = final.IndexOf("\n");
                string Timename = final.Substring(0, div2).Replace("/span>","");
                final = final.Remove(0, div2 + 1);

                div2 = final.IndexOf("\n");
                string AQscore = "(客)" + final.Substring(0, div2).Replace("/"," ");
                final = final.Remove(0, div2 + 1);

                div2 = final.IndexOf("\n");
                string HQscore = "(主)" + final.Substring(0, div2).Replace("/", " ");
                final = final.Remove(0, div2 + 1);

                listBox1.Items.Add(Aname);
                listBox1.Items.Add(Hname);
                listBox1.Items.Add(Timename);
                listBox1.Items.Add(matchScore);
                listBox1.Items.Add(AQscore);
                listBox1.Items.Add(HQscore);
                listBox1.Items.Add(" ");
            }


        }

    }
}
