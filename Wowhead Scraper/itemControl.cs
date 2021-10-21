using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;

namespace Wowhead_Scraper
{
    public partial class itemControl : UserControl
    { 

        List<string> sqlFile = new List<string>();

        private volatile int progress = 0;
        private volatile bool isFirstFinishd = false;
        private volatile bool isSecondFinishd = false;

        public itemControl()
        {
            InitializeComponent();
        }

        public static int Truth(params bool[] booleans)
        {
            return booleans.Count(b => b);
        }


        private wItem PrepareTexts(string itemName, string itemDescription)
        {
            wItem w = new wItem();
            w.itemName = itemName;
            w.itemDescription = itemDescription; 

            // Preparing questName..

            w.itemName = w.itemName.Replace("[DEPRECATED]", ""); // Replaces [DEPRECATED] If any
            w.itemName = w.itemName.Replace("[DEPRECATED] ", ""); // Replaces [DEPRECATED] (with spaces) If any
            w.itemName = w.itemName.Replace("[UNUSED] ", ""); // Replaces [UNUSED] If any...
            w.itemName = w.itemName.Replace("'", @"\'");

            // Preparing questText..

            w.itemDescription = w.itemDescription.Replace("&lt;", "<");
            w.itemDescription = w.itemDescription.Replace("&gt;", ">");
            w.itemDescription = w.itemDescription.Replace("&nbsp;", " ");
            w.itemDescription = Regex.Replace(w.itemDescription, @"\r\n?|\n", "");
            w.itemDescription = Regex.Replace(w.itemDescription, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);
            w.itemDescription = w.itemDescription.Replace("'", @"\'");
             

            return w;
        }


        private void MakeSQLStarting()
        {
            sqlFile.Add("-- DO NOT REMOVE THESE LINES");
            sqlFile.Add("--BEGIN");
            if (chkItemName.Checked == true)
                sqlFile.Add("-- ItemName=TRUE");
            else
                sqlFile.Add("-- ItemName=FALSE");
            if (chkItemDesc.Checked == true)
                sqlFile.Add("-- ItemDescription=TRUE");
            else
                sqlFile.Add("-- ItemDescription=FALSE"); 

            sqlFile.Add("--END");
            sqlFile.Add(Environment.NewLine);
        }

        private void MakeSQL(int i, string itemName, string itemDescription)
        {
            wItem t = PrepareTexts(itemName, itemDescription);

            //   sqlFile[i] = "UPDATE locales_quest SET Title_loc6='" + t.questName + "', Details_loc6='" + t.questText +  "', Objectives_loc6='" + t.questObjectives + "', OfferRewardText_loc6='" + t.questComplete + "', RequestItemsText_loc6='" + t.questProgress + "', EndText_loc6='', CompletedText_loc6='', ObjectiveText1_loc6='', ObjectiveText2_loc6='' WHERE Id='" + i.ToString() + "';";

            sqlFile.Add("--");
            sqlFile.Add("-- SQL data for the item id: " + i.ToString() + " and with the following name: " + t.itemName + "");
            sqlFile.Add("--");
            sqlFile.Add(Environment.NewLine); 

            if (chkItemName.Checked == true)
            {
                if (chkItemDesc.Checked == true)
                {
                    sqlFile.Add("UPDATE item_template SET name='"+ t.itemName +"' WHERE entry='"+ i.ToString() +"';");
                    if (t.itemDescription != "")
                        sqlFile.Add("UPDATE item_template SET description='" + t.itemDescription + "' WHERE entry='" + i.ToString() + "';");
                    else
                        sqlFile.Add("-- No item data found for itemDescription for the query: UPDATE item_template SET description = '' WHERE ID = '" + i.ToString() + "'; ");

                }
                else
                {
                    sqlFile.Add("UPDATE item_template SET name='" + t.itemName + "' WHERE entry='" + i.ToString() + "';");
                    if (t.itemDescription != "")
                        sqlFile.Add("--UPDATE item_template SET description='" + t.itemDescription + "' WHERE entry='" + i.ToString() + "';");
                    else
                        sqlFile.Add("-- No item data found for itemDescription for the query: UPDATE item_template SET description = '' WHERE ID = '" + i.ToString() + "'; ");

                }
            }
            else
            {
                if (chkItemDesc.Checked == true)
                {
                    sqlFile.Add("--UPDATE item_template SET name='" + t.itemName + "' WHERE entry='" + i.ToString() + "';");
                    if (t.itemDescription != "")
                        sqlFile.Add("UPDATE item_template SET description='" + t.itemDescription + "' WHERE entry='" + i.ToString() + "';");
                    else
                        sqlFile.Add("-- No item data found for itemDescription for the query: UPDATE item_template SET description = '' WHERE ID = '" + i.ToString() + "'; ");

                }
                else
                {
                    sqlFile.Add("--UPDATE item_template SET name='" + t.itemName + "' WHERE entry='" + i.ToString() + "';");
                    if (t.itemDescription != "")
                        sqlFile.Add("--UPDATE item_template SET description='" + t.itemDescription + "' WHERE entry='" + i.ToString() + "';");
                    else
                        sqlFile.Add("-- No item data found for itemDescription for the query: UPDATE item_template SET description = '' WHERE ID = '" + i.ToString() + "'; ");

                }
            }


            sqlFile.Add(Environment.NewLine);
        }
         

        private void ClearAll()
        {
            progress = 0;
            progressBar1.Value = progressBar1.Minimum;
            isFirstFinishd = false;
            isSecondFinishd = false;
            sqlFile.Clear();
        }

        private void SaveFile()
        {
            string filename = "itemData-" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + ".sql";
            if (!File.Exists(filename))
            {
                System.IO.File.WriteAllLines(filename, sqlFile);
                MessageBox.Show("File: " + filename + " Saved");
                ClearAll();
            }
            else
            {
                if (MessageBox.Show(this, "You already have a file with the same name, Do you want to rewrite the file?", "Alert" , MessageBoxButtons.YesNo) == DialogResult.Yes);
                {
                    System.IO.File.WriteAllLines(filename, sqlFile);
                    MessageBox.Show("File: " + filename + " Saved");
                    ClearAll();
                }
            }
        }

        private void BeginParse(List<int> IndexList, int threadId)
        {
            for (int k = 0; k < IndexList.Count(); k++)
            {
                if (IndexList[k] != 0)
                {
                    try
                    {
                        XmlReader reader = XmlReader.Create("http://us.battle.net/wow/" + "pt" + "/item/" + IndexList[k].ToString());
                        wItem obj = new wItem();
                        while (!reader.Value.Contains("color-q"))
                        {
                            reader.ReadToFollowing("h2");
                            reader.MoveToAttribute("class");
                            if (reader.EOF)
                            {
                                break;
                            }
                        }
                        reader.MoveToElement();
                        string nameItem = reader.ReadElementContentAsString().Trim().Replace("\u0027", "\u005c\u0027");
                        if (nameItem.Contains("[Monster]")) // Les noms d'objets placés entre crochets ne sont pas traduits
                        {
                            //  _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Objet " + id.ToString() + " non traduit !");
                        }
                        else
                        {
                            obj.Id = IndexList[k];
                            obj.itemName = nameItem;
                            //  _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Objet :" + id.ToString() + " Nom : " + nameItem);
                            reader.ReadToFollowing("li");
                            while (!reader.Value.Contains("color-tooltip-yellow"))
                            {
                                reader.ReadToFollowing("li");
                                reader.MoveToAttribute("class");
                                if (reader.EOF)
                                {
                                    break;
                                }
                            }
                            if (!reader.EOF)
                            {
                                reader.MoveToElement();
                                string descItem = reader.ReadElementContentAsString().Trim().Replace("\u0027", "\u005c\u0027").Replace("\u0022", string.Empty);
                                // _formulaire.textBoxAvancement.AppendText(" Description : " + descItem);
                                obj.itemDescription = descItem;
                            }
                        }

                        if (obj.itemDescription == null)
                            obj.itemDescription = "";

                        MakeSQL(IndexList[k], obj.itemName, obj.itemDescription); 
                    try
                        {
                            progress++;

                        }
                        catch { }
                    }
                    catch
                    {
                        try
                        {
                            progress++;
                        }
                        catch { }
                    }
                }
                
            }

            if (threadId == 1)
                isFirstFinishd = true;
            else if (threadId == 2)
                isSecondFinishd = true;
        }

        private void Parse_Click(object sender, EventArgs e)
        {
            MakeSQLStarting(); // The head of the sql file
            //                sqlFile = new string[Convert.ToInt32(EndId.Text) + 1];
            if (sItem.Checked == true)
            {

                if (Convert.ToInt32(beginId.Text) > 0)
                {
                    try
                    {
                    XmlReader reader = XmlReader.Create("http://us.battle.net/wow/" + "pt" + "/item/" + beginId.Text.ToString());
                    wItem obj = new wItem();
                    while (!reader.Value.Contains("color-q"))
                    {
                        reader.ReadToFollowing("h2");
                        reader.MoveToAttribute("class");
                        if (reader.EOF)
                        {
                            break;
                        }
                    }
                    reader.MoveToElement();
                    string nameItem = reader.ReadElementContentAsString().Trim().Replace("\u0027", "\u005c\u0027");
                    if (nameItem.Contains("[Monster]"))
                    {

                    }
                    else
                    {
                        obj.Id = Convert.ToInt32(beginId.Text);
                        obj.itemName = nameItem;
                        reader.ReadToFollowing("li");
                        while (!reader.Value.Contains("color-tooltip-yellow"))
                        {
                            reader.ReadToFollowing("li");
                            reader.MoveToAttribute("class");
                            if (reader.EOF)
                                { 
                                    break;
                                }
                        }
                        if (!reader.EOF)
                        {
                            reader.MoveToElement();
                            string descItem = reader.ReadElementContentAsString().Trim().Replace("\u0027", "\u005c\u0027").Replace("\u0022", string.Empty);
                            obj.itemDescription = descItem;
                        }
                        }

                        if (obj.itemDescription == null)
                            obj.itemDescription = ""; 

                        MakeSQL(Convert.ToInt32(beginId.Text), obj.itemName, obj.itemDescription);
                        progressBar1.Value = progressBar1.Maximum;
                        SaveFile();
                    }
                     catch { }
                }
                else
                {
                    MessageBox.Show("Object Id cannot be 0 (zero)");
                    sqlFile.Clear();
                }
            }
            else if (mItem.Checked == true)
            {

                if ((Convert.ToInt32(beginId.Text) > 0) && (Convert.ToInt32(EndId.Text) > 0))
                {
                    timer1.Start();
                    progressBar1.Maximum = Convert.ToInt32(EndId.Text);
                    List<int> firstThread = new List<int>();
                    List<int> secondThread = new List<int>();

                    for (int i = Convert.ToInt32(beginId.Text); i < Convert.ToInt32(EndId.Text) + 1; i++)
                    {
                        if (i % 2 == 0)
                        {
                            secondThread[i] = i;
                        }
                        else
                        {
                            firstThread[i] = i;
                        }
                    }

                    timer1.Start();

                    new Thread(() => { BeginParse(firstThread, 1); }).Start();
                    new Thread(() => { BeginParse(secondThread, 2); }).Start();
                }
                else
                {
                    MessageBox.Show("Some of the values are incorrect. First Id and Last Id cannot be 0 (zero)");
                    sqlFile.Clear();
                }
            }
            else if(rItem.Checked == true)
            { 
                
                    OpenFileDialog ofd = new OpenFileDialog();
                    List<string> file = new List<string>();
                    ofd.Filter = "Range Items | *.rng";

                    var result = ofd.ShowDialog(); 

                    if (result.ToString() == "OK")
                    {
                        file = File.ReadAllLines(ofd.FileName).ToList(); 
                    }

                    progressBar1.Maximum = file.Count;
                    List <int> firstThread = new List<int>();
                    List<int> secondThread = new List<int>();

 
                    for (int i = 0; i < file.Count; i++)
                    { 
                        if (Convert.ToInt32(file[i]) != 0)
                        {
                         
                            if (i % 2 == 0)
                            {
                                secondThread.Add(Convert.ToInt32(file[i])); 
                            }
                            else
                            {
                                firstThread.Add(Convert.ToInt32(file[i])); 
                            }
                        }
                    }

                    timer1.Start(); 

                    new Thread(() => { BeginParse(firstThread, 1); }).Start();
                    new Thread(() => { BeginParse(secondThread, 2); }).Start();
                 
            }
            } 
        private void mItem_CheckedChanged(object sender, EventArgs e)
        {
            if(mItem.Checked == true)
            {
                lblLastID.Visible = true;
                EndId.Visible = true;
                lblObjID.Text = "First Id:";
            }
        }

        private void sItem_CheckedChanged(object sender, EventArgs e)
        {
            if(sItem.Checked == true)
            {
                lblLastID.Visible = false;
                EndId.Visible = false;
                lblObjID.Text = "Object Id:";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = progress;
            status.Text = progress.ToString() + "/" + progressBar1.Maximum;

            if(Truth(new bool[] { isSecondFinishd, isFirstFinishd }) == 2)
            {
                progressBar1.Value = progressBar1.Maximum;
                timer1.Stop();
                SaveFile();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            type.SelectedItem = type.Items[0]; 
        }

        private void beginId_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void EndId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void status_TextChanged(object sender, EventArgs e)
        {
            status.Left = (this.ClientSize.Width - status.Width) / 2;
        }

        private void itemControl_Resize(object sender, EventArgs e)
        {
            status.Left = (this.ClientSize.Width - status.Width) / 2; 
        }

        private void RItem_CheckedChanged(object sender, EventArgs e)
        {

        }
    }


    public class wItem
    {
        public int Id { get; set; }
        public string itemName { get; set; } 
        public string itemDescription { get; set; } 

    } 

}
