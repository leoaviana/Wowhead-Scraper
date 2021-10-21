using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Wowhead_Scraper
{
    public partial class questControl : UserControl
    {
        List<string> firstThreadStrings = new List<string>();
        List<string> secondThreadStrings = new List<string>();
        List<string> thirdThreadStrings = new List<string>();
        List<string> forthThreadStrings = new List<string>();

        OpenFileDialog ofd;

        List<string> sqlFile = new List<string>();
        List<string> logFile = new List<string>();

        private volatile int progress = 0;
        private volatile bool isFirstFinishd = false;
        private volatile bool isSecondFinishd = false;
        private volatile bool isThirdFinishd = false;
        private volatile bool isForthFinishd = false; 

        private int fstSizes = 0;
        private int frthSize = 0;
        private int maxItemValue = 0;
        private volatile bool issql = false;

        public questControl()
        {
            InitializeComponent();
        }

        public static int Truth(params bool[] booleans)
        {
            return booleans.Count(b => b);
        }

        private string ReplaceDBStrings(int questId, string qText, string startString, string endString)
        {
            wQuest w = new wQuest();

            string returnTxt = qText;
            string text = qText; 
            int indexStart = 0, indexEnd = 0;
            bool exit = false;

            try
            {
                while (!exit)
                {
                    indexStart = text.IndexOf(startString);
                    indexEnd = text.IndexOf(endString);
                    if (indexStart != -1 && indexEnd != -1)
                    {
                        string parsedText = text.Substring(indexStart + startString.Length,
                            indexEnd - indexStart - startString.Length);

                        if (parsedText.Contains("/") == true)
                        {
                            string[] splitText = parsedText.Split('/');

                            if (issql)
                            {
                                returnTxt = returnTxt.Replace(parsedText, "$g" + splitText[0] + ":" + splitText[1] + ";");
                            }
                            else
                            {
                                returnTxt = returnTxt.Replace(parsedText, "YOUR_GENDER(" + splitText[0] + ";" + splitText[1] + ")");
                            }

                            text = text.Substring(indexEnd + endString.Length);
                        }
                        else
                        {
                            for (int i = 0; i < w.knownStrings.Count(); i++)
                            {
                                if (parsedText == w.knownStrings[i])
                                {
                                    if (issql)
                                    {
                                        returnTxt = returnTxt.Replace("<" + parsedText + ">", w.dbStrings[i]);
                                    }
                                    else
                                    {
                                        returnTxt = returnTxt.Replace("<" + parsedText + ">", w.addOnStrings[i]);
                                    }
                                }
                            }
                            text = text.Substring(indexEnd + endString.Length);
                        }
                    }
                    else
                        exit = true;
                }

               returnTxt = returnTxt.Replace("<", "");
               returnTxt = returnTxt.Replace(">", "");
            }
            catch
            {
                logFile.Add(DateTime.Now.ToString() + "   : The quest with id: " + questId + " may need further work on DBStrings replacing.");
            }

            return returnTxt;
        }

        private wQuest PrepareTexts(int questId, string questName, string questObjectives, string questText, string questProgress, string questComplete)
        {
            wQuest w = new wQuest();
            w.questName = questName;
            w.questObjectives = questObjectives;
            w.questText = questText;
            w.questProgress = questProgress;
            w.questComplete = questComplete;

            // Preparing questName..

            w.questName = w.questName.Replace("[DEPRECATED]", ""); // Replaces [DEPRECATED] If any
            w.questName = w.questName.Replace("[DEPRECATED] ", ""); // Replaces [DEPRECATED] (with spaces) If any
            w.questName = w.questName.Replace("[UNUSED] ", ""); // Replaces [UNUSED] If any...

            w.questName = w.questName.Replace("&lt;", "<");
            w.questName = w.questName.Replace("&gt;", ">");
            w.questName = w.questName.Replace("&quot;", "\"");
            w.questName = w.questName.Replace("&nbsp;", " ");
            w.questName = Regex.Replace(w.questName, @"\r\n?|\n", "");
            w.questName = Regex.Replace(w.questName, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);



            if (issql)
                w.questName = w.questName.Replace("'", @"\'");

            if (!issql)
            {
                char[] qN = new char[w.questName.Length];
                bool open = true;
                qN = w.questName.ToCharArray();
                for (int i = 0; i < w.questName.Length; i++)
                {
                   if(qN[i] == '"')
                    {
                        if (open == true)
                            qN[i] = '<';
                        else
                            qN[i] = '>';

                        open = (open == true) ? false : true;
                    }
                }

                w.questName = new string(qN);
            }

            // Preparing questText..

            w.questText = w.questText.Replace("&lt;", "<");
            w.questText = w.questText.Replace("&gt;", ">");
            w.questText = w.questText.Replace("&quot;", "\"");
            w.questText = w.questText.Replace("&nbsp;", " ");
            w.questText = Regex.Replace(w.questText, @"\r\n?|\n", "");
            w.questText = Regex.Replace(w.questText, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);
            if(issql)
                w.questText = w.questText.Replace("'", @"\'");


            w.questText = Regex.Replace(w.questText, "<a\\s+(?:[^>]*?\\s+)?href=([\"'])(.*?)\\1>(.*?)</a>", "$3");
            w.questText = ReplaceDBStrings(questId, w.questText, "<", ">");

            if (!issql)
            {
                char[] qN = new char[w.questText.Length];
                bool open = true;
                qN = w.questText.ToCharArray();
                for (int i = 0; i < w.questText.Length; i++)
                {
                    if (qN[i] == '"')
                    {
                        if (open == true)
                            qN[i] = '<';
                        else
                            qN[i] = '>';

                        open = (open == true) ? false : true;
                    }
                } 
                w.questText = new string(qN);
            }

            // Preparing questProgress..

            w.questProgress = w.questProgress.Replace("&lt;", "<");
            w.questProgress = w.questProgress.Replace("&gt;", ">");
            w.questProgress = w.questProgress.Replace("&quot;", "\"");
            w.questProgress = w.questProgress.Replace("&nbsp;", " ");
            w.questProgress = Regex.Replace(w.questProgress, @"\r\n?|\n", "");
            w.questProgress = Regex.Replace(w.questProgress, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);
            if(issql)
                w.questProgress = w.questProgress.Replace("'", @"\'");


            w.questProgress = Regex.Replace(w.questProgress, "<a\\s+(?:[^>]*?\\s+)?href=([\"'])(.*?)\\1>(.*?)</a>", "$3");

            w.questProgress = ReplaceDBStrings(questId, w.questProgress, "<", ">");

            if (!issql)
            {
                char[] qN = new char[w.questProgress.Length];
                bool open = true;
                qN = w.questProgress.ToCharArray();
                for (int i = 0; i < w.questProgress.Length; i++)
                {
                    if (qN[i] == '"')
                    {
                        if (open == true)
                            qN[i] = '<';
                        else
                            qN[i] = '>';

                        open = (open == true) ? false : true;
                    }
                }
                w.questProgress = new string(qN);
            }

            // Preparing questComplete..

            w.questComplete = w.questComplete.Replace("&lt;", "<");
            w.questComplete = w.questComplete.Replace("&gt;", ">");
            w.questComplete = w.questComplete.Replace("&quot;", "\"");
            w.questComplete = w.questComplete.Replace("&nbsp;", " ");
            w.questComplete = Regex.Replace(w.questComplete, @"\r\n?|\n", "");
            w.questComplete = Regex.Replace(w.questComplete, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);
            if(issql)
                w.questComplete = w.questComplete.Replace("'", @"\'");

            w.questComplete = Regex.Replace(w.questComplete, "<a\\s+(?:[^>]*?\\s+)?href=([\"'])(.*?)\\1>(.*?)</a>", "$3");

            w.questComplete = ReplaceDBStrings(questId, w.questComplete, "<", ">");

            if (!issql)
            {
                char[] qN = new char[w.questComplete.Length];
                bool open = true;
                qN = w.questComplete.ToCharArray();
                for (int i = 0; i < w.questComplete.Length; i++)
                {
                    if (qN[i] == '"')
                    {
                        if (open == true)
                            qN[i] = '<';
                        else
                            qN[i] = '>';

                        open = (open == true) ? false : true;
                    }
                }
                w.questComplete = new string(qN);
            }

            // Preparing questObjectives..

            w.questObjectives = w.questObjectives.Replace("&quot;", "\"");
            w.questObjectives = Regex.Replace(w.questObjectives, @"\r\n?|\n", "");
            w.questObjectives = Regex.Replace(w.questObjectives, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);
            if(issql)
                w.questObjectives = w.questObjectives.Replace("'", @"\'");

            if (!issql)
            {
                char[] qN = new char[w.questObjectives.Length];
                bool open = true;
                qN = w.questObjectives.ToCharArray();
                for (int i = 0; i < w.questObjectives.Length; i++)
                {
                    if (qN[i] == '"')
                    {
                        if (open == true)
                            qN[i] = '<';
                        else
                            qN[i] = '>';

                        open = (open == true) ? false : true;
                    }
                }
                w.questObjectives = new string(qN);
            }
             

            return w;
        }


        private void MakeSQLStarting()
        {
            if (issql)
            {
                sqlFile.Add("--");
                if (chkAllowObjectives.Checked == true)
                    sqlFile.Add("-- Objectives=TRUE");
                else
                    sqlFile.Add("-- Objectives=FALSE");

                if (chkRewardText.Checked == true)
                    sqlFile.Add("-- RewardText=TRUE");
                else
                    sqlFile.Add("-- RewardText=FALSE");

                if (chkQuestName.Checked == true)
                    sqlFile.Add("-- QuestName=TRUE");
                else
                    sqlFile.Add("-- QuestName=FALSE");
                sqlFile.Add("--");
                sqlFile.Add(Environment.NewLine);
            }
        }

        private void MakeSQL(int i, string questName, string questObjectives, string questText, string questProgress, string questComplete, List<String> sqlFile)
        {
            wQuest t = PrepareTexts(i, questName, questObjectives, questText, questProgress, questComplete);

            if (issql)
            {
                //   sqlFile[i] = "UPDATE locales_quest SET Title_loc6='" + t.questName + "', Details_loc6='" + t.questText +  "', Objectives_loc6='" + t.questObjectives + "', OfferRewardText_loc6='" + t.questComplete + "', RequestItemsText_loc6='" + t.questProgress + "', EndText_loc6='', CompletedText_loc6='', ObjectiveText1_loc6='', ObjectiveText2_loc6='' WHERE Id='" + i.ToString() + "';";

                sqlFile.Add("--");
                sqlFile.Add("-- SQL data for the quest id: " + i.ToString() + " and with the following title: " + t.questName + "");
                sqlFile.Add("--");
                sqlFile.Add(Environment.NewLine);



                if (chkQuestName.Checked == true)
                    sqlFile.Add("UPDATE quest_template SET LogTitle='" + t.questName + "' WHERE ID='" + i.ToString() + "';");
                else
                    sqlFile.Add("--UPDATE quest_template SET LogTitle='" + t.questName + "' WHERE ID='" + i.ToString() + "';");

                if (chkAllowObjectives.Checked == true)
                    sqlFile.Add("UPDATE quest_template SET LogDescription='" + t.questObjectives + "' WHERE ID='" + i.ToString() + "';");
                else
                    sqlFile.Add("--UPDATE quest_template SET LogDescription='" + t.questObjectives + "' WHERE ID='" + i.ToString() + "';");
                sqlFile.Add("UPDATE quest_template SET QuestDescription='" + t.questText + "' WHERE ID='" + i.ToString() + "';");


                if (t.questProgress != "")
                    sqlFile.Add("UPDATE quest_request_items SET CompletionText='" + t.questProgress + "' WHERE ID='" + i.ToString() + "';");
                else
                    sqlFile.Add("-- No quest data found for CompletionText for the query:  UPDATE quest_request_items SET CompletionText='' WHERE ID='" + i.ToString() + "';");
                if (t.questComplete != "")
                    sqlFile.Add("UPDATE quest_offer_reward SET RewardText='" + t.questComplete + "' WHERE ID='" + i.ToString() + "';");
                else
                    sqlFile.Add("-- No quest data found for RewardText for the query: UPDATE quest_offer_reward SET RewardText = '' WHERE ID = '" + i.ToString() + "'; ");
                sqlFile.Add(Environment.NewLine);
            }
            else
            {
                sqlFile.Add("[\"" + i.ToString() + "\"]={[\"Title\"]=\"" + t.questName + "\", [\"Objectives\"]=\"" + t.questObjectives + "\", [\"Description\"]=\""
                    + t.questText + "\", [\"Progress\"]=\"" + t.questProgress + "\", [\"Completion\"]=\"" + t.questComplete + "\", [\"Translator\"]=\"https://github.com/leoaviana/ using his own Wowhead scraper v2\"},");
 

            }
        }
         

        private void ClearAll()
        {
            progress = 0;
            fstSizes = 0;
            frthSize = 0;

            progressBar1.Value = progressBar1.Minimum;
            isFirstFinishd = false;
            isSecondFinishd = false;
            isThirdFinishd = false;
            isForthFinishd = false;


            sqlFile.Clear();
            firstThreadStrings.Clear();
            secondThreadStrings.Clear();
            thirdThreadStrings.Clear();
            forthThreadStrings.Clear();
        }

        private void SaveFile()
        {
            string filename = ""; 
            if (issql)
            {
                filename = "./questData-" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + ".sql";
            }
            else
            {
                filename = "./questData-" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + ".lua";
            }
            if (!File.Exists(filename))
            {
                System.IO.File.WriteAllLines(filename, sqlFile);
                System.IO.File.WriteAllLines(filename.Replace(".lua", ".log").Replace(".sql", ".log"), logFile);

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

        private void BeginParse(int[] IndexList, int threadId)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            for (int k = 0; k < IndexList.Count(); k++)
            {
                if (IndexList[k] != 0)
                {
                   try
                   {
                        string whUrl = "http://pt.tbc.wowhead.com/quest=" + IndexList[k].ToString(); 
                        bool retryWithNewest = false;
                        bool retryWithOldest = false;

                        RETRY:
                        HtmlWeb web = new HtmlWeb();

                        HtmlAgilityPack.HtmlDocument document = web.Load(whUrl);

                        string htmlPart = "//div[@id='main-contents']//div[@class='text']";

                        string questName = document.DocumentNode.SelectSingleNode(htmlPart + "//h1[@class='heading-size-1']").InnerText;

                        if (questName == "Missões")
                        {
                            if (retryWithOldest == true && retryWithNewest == true)
                                goto SaidaDoLoop; // quest not found, going to the next ID
                            else
                            {
                                if(retryWithOldest == false)
                                { 
                                    retryWithOldest = true;
                                    whUrl = "http://pt.classic.wowhead.com/quest=" + IndexList[k].ToString();
                                    goto RETRY; //Quest not available in TBC, but probably exists on classic? retrying...
                                }
                                else
                                { 
                                    retryWithNewest = true;
                                    whUrl = "http://pt.wowhead.com/quest=" + IndexList[k].ToString();
                                    goto RETRY;      // since both tbc and classic versions returned error, retrying with retail data (9.1)
                                }
                            }
                        }

                        var questObjectivess = document.DocumentNode.SelectSingleNode(htmlPart + "//h1[@class='heading-size-1']").NextSibling.NextSibling.NextSibling;
                        var questObjectives = "";

                        if (questObjectivess.NextSibling.InnerText != "Progresso")
                        {
                            while (questObjectivess.Name != "table" && questObjectivess.Name != "div" && questObjectivess.Name != "h2")
                            {
                                questObjectives += questObjectivess.InnerText;
                                questObjectivess = questObjectivess.NextSibling;
                            }
                        }

                        bool alreadyCaught = false;

                    questObjectiveCheck:

                        var possible1 = "Essa missão foi marcada como obsoleta pela Blizzard e não pode ser adquirida ou completada.";
                        var possible2 = "\nEssa missão foi marcada como obsoleta pela Blizzard e não pode ser adquirida ou completada.";
                        var possible3 = "Essa missão não está mais disponível no jogo";
                        var possible4 = "\nEssa missão não está mais disponível no jogo";


                        if ((questObjectives == possible1 || questObjectives == possible2) || (questObjectives == possible3 || questObjectives == possible4)) // obsolete mission, the quest objectives is above another div in html
                        {
                            questObjectives = "";
                            questObjectivess = document.DocumentNode.SelectSingleNode(htmlPart + "//div[@class='pad']").NextSibling;
                            while (questObjectivess.Name != "table" && questObjectivess.Name != "div" && questObjectivess.Name != "h2")
                            {
                                questObjectives += questObjectivess.InnerText;
                                questObjectivess = questObjectivess.NextSibling;
                            }
                        }

                        HtmlNode questTexts;

                        string questProgress = ""; 
                        string questComplete = "";
                        string questText = "";
                        

                        if ((questObjectives == "" || questObjectives == "\n") || (questObjectives == " " || questObjectives == null))
                        {
                            try
                             {
                                if (document.DocumentNode.SelectSingleNode(htmlPart + "//h2[@class='heading-size-3' and @classes='first']").InnerText == "Completo")
                                {
                                    questProgress = "";
                                    var questCompletee = document.DocumentNode.SelectSingleNode(htmlPart + "//h2[@class='heading-size-3' and @classes='first']").NextSibling;
                                    while (questCompletee.Name != "table" && questCompletee.Name != "div" && questCompletee.Name != "h2")
                                    {
                                        questComplete += questCompletee.OuterHtml;
                                        questCompletee = questCompletee.NextSibling;
                                    }
                                    questComplete = questComplete.Replace("<div id=\"lknlksndgg-completion\" style=\"display: none\">", "");


                                    questComplete = questComplete.Replace("</div>", "");
                                    if (issql)
                                        questComplete = questComplete.Replace("<br>", "$B"); // equivalent to new line on trinity database 
                                    else
                                        questComplete = questComplete.Replace("<br>", "NEW_LINE"); // equivalent to new line on questtradutor database 
                                }
                                else
                                {
                                    try
                                    {
                                        var questProgresss = document.DocumentNode.SelectSingleNode(htmlPart + "//h2[@class='heading-size-3' and @classes='first']").NextSibling;
                                        while (questProgresss.Name != "table" && questProgresss.Name != "div" && questProgresss.Name != "h2")
                                        {
                                            questProgress += questProgresss.OuterHtml;
                                            questProgresss = questProgresss.NextSibling;
                                        }
                                    }
                                    catch {}
                                    questComplete = document.DocumentNode.SelectSingleNode(htmlPart + "//div[@id='lknlksndgg-completion']").OuterHtml;
                                    questComplete = questComplete.Replace("<div id=\"lknlksndgg-completion\" style=\"display: none\">", ""); 

                                    questComplete = questComplete.Replace("</div>", "");
                                    if (issql)
                                        questComplete = questComplete.Replace("<br>", "$B"); // equivalent to new line on trinity database 
                                    else
                                        questComplete = questComplete.Replace("<br>", "NEW_LINE"); // equivalent to new line on questtradutor database 

                                }
                          }
                          catch
                          { 
                            ///  goto SaidaDoLoop;
                            try
                            {
                                if (alreadyCaught) 
                                    goto SaidaDoLoop; 
                                questObjectives = document.DocumentNode.SelectSingleNode(htmlPart + "//h1[@class='heading-size-1']").NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                                if (questObjectives == "Essa missão não está mais disponível no jogo") 
                                {
                                    questObjectives = document.DocumentNode.SelectSingleNode(htmlPart + "//h1[@class='heading-size-1']").NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                                }

                                if (questObjectives == "Recompensas")
                                    goto SaidaDoLoop;

                                alreadyCaught = true;
                                goto questObjectiveCheck;
                            } catch
                            {
                                goto SaidaDoLoop;
                            }
                          }

                        } 
                        else
                        {

                            questTexts = document.DocumentNode.SelectSingleNode(htmlPart + "//h2[@class='heading-size-3']").NextSibling;


                            try
                            {
                                try
                                {
                                    questProgress = document.DocumentNode.SelectSingleNode(htmlPart + "//div[@id='lknlksndgg-progress']").InnerText;
                                }
                                catch { }
                                questComplete = document.DocumentNode.SelectSingleNode(htmlPart + "//div[@id='lknlksndgg-completion']").OuterHtml;
                                questComplete = questComplete.Replace("<div id=\"lknlksndgg-completion\" style=\"display: none\">", ""); 

                                questComplete = questComplete.Replace("</div>", "");
                                if (issql)
                                    questComplete = questComplete.Replace("<br>", "$B"); // equivalent to new line in trinity database 
                                else
                                    questComplete = questComplete.Replace("<br>", "NEW_LINE"); // equivalent to new line in wow database 
                            }
                            catch
                            {
                            }

                            List<HtmlNode> qDescription = new List<HtmlNode>();

                            while (questTexts != null && questTexts.Name != "h2" && questTexts.Name != "div")
                            {
                                qDescription.Add(questTexts);
                                questTexts = questTexts.NextSibling;
                            }

                            StringBuilder strBuilder = new StringBuilder();
                            for (int j = 0; j < qDescription.Count; j++)
                            {

                                if (qDescription[j].OuterHtml == "<br />")
                                {
                                    if (issql)
                                        strBuilder.Append("$B"); // equivalent to new line in trinity database
                                    else
                                        strBuilder.Append("NEW_LINE"); // equivalent to new line in trinity database
                                }
                                else
                                {
                                    strBuilder.Append(qDescription[j].OuterHtml);
                                }
                            }

                            questText = strBuilder.ToString(); 
                        }

                        if (questText.Contains("Completando essa missão você ganhará:") || questText.Contains("Você irá receber:") || questText.Contains("Você vai poder escolher uma dessas recompensas:"))
                            goto SaidaDoLoop;

                        if (threadId == 1)
                            MakeSQL(IndexList[k], questName, questObjectives, questText, questProgress, questComplete, firstThreadStrings);
                        else if (threadId == 2)
                            MakeSQL(IndexList[k], questName, questObjectives, questText, questProgress, questComplete, secondThreadStrings);
                        else if (threadId == 3)
                            MakeSQL(IndexList[k], questName, questObjectives, questText, questProgress, questComplete, thirdThreadStrings);
                        else if (threadId == 4)
                            MakeSQL(IndexList[k], questName, questObjectives, questText, questProgress, questComplete, forthThreadStrings);

                        SaidaDoLoop:

                        try
                        {
                            progress++;

                        }
                        catch { }
                         
                    } //
                     catch(Exception exe)
                    { 
                       try
                       {
                          progress++;
                        }
                         catch { }
                     } //
                       
                }
                
            }

            if (threadId == 1)
                isFirstFinishd = true;
            else if (threadId == 2)
                isSecondFinishd = true;
            else if (threadId == 3)
                isThirdFinishd = true;
            else if (threadId == 4)
                isForthFinishd = true;
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

                        string whUrl = "http://pt.classic.wowhead.com/quest=" + beginId.Text;
                        bool retryWithNewest = false;

                        RETRY:

                        HtmlWeb web = new HtmlWeb();

                        HtmlAgilityPack.HtmlDocument document = web.Load(whUrl);

                        string htmlPart = "//div[@id='main-contents']//div[@class='text']";

                        string questName = document.DocumentNode.SelectSingleNode(htmlPart + "//h1[@class='heading-size-1']").InnerText;

                        if (questName == "Missões")
                        {
                            if (retryWithNewest == true)
                                goto SaidaDoLoop; // se falhar. continuaremos para a próxima quest, se existir. 
                            else
                            {
                                retryWithNewest = true;
                                whUrl = "http://pt.wowhead.com/quest=" + beginId.Text;
                                goto RETRY;      // significa q nao exsite esas quest no site. tentaremos no site com a versão 8.0
                            }
                        }

                        string questObjectives = document.DocumentNode.SelectSingleNode(htmlPart + "//h1[@class='heading-size-1']").NextSibling.NextSibling.NextSibling.NextSibling.InnerText;

                        if (questObjectives == "Essa missão foi marcada como obsoleta pela Blizzard e não pode ser adquirida ou completada.") // obsolete mission, the quest objectives is above another div in html
                        {
                            questObjectives = document.DocumentNode.SelectSingleNode(htmlPart + "//div[@class='pad']").NextSibling.InnerText;
                        }

                        HtmlNode questTexts;

                        string questProgress = ""; // lol
                        string questComplete = ""; // bur
                        string questText = ""; // kek

                        if ((questObjectives == "" || questObjectives == "\n") || (questObjectives == " " || questObjectives == null))
                        {
                            try
                            {
                                if (document.DocumentNode.SelectSingleNode(htmlPart + "//h2[@class='heading-size-3 first']").InnerText == "Completo")
                                {
                                    questProgress = "";
                                    questComplete = document.DocumentNode.SelectSingleNode(htmlPart + "//h2[@class='heading-size-3 first']").NextSibling.OuterHtml; 
                                    questComplete = questComplete.Replace("<div id=\"lknlksndgg-completion\" style=\"display: none\">", "");

                                    questComplete = questComplete.Replace("</div>", "");
                                    if (issql)
                                        questComplete = questComplete.Replace("<br>", "$B"); // equivalent to new line in trinity database 
                                    else
                                        questComplete = questComplete.Replace("<br>", "NEW_LINE"); // equivalent to new line in wow database 
                                }
                                else
                                {
                                    questProgress = document.DocumentNode.SelectSingleNode(htmlPart + "//h2[@class='heading-size-3 first']").NextSibling.InnerText;
                                    questComplete = document.DocumentNode.SelectSingleNode(htmlPart + "//div[@id='lknlksndgg-completion']").OuterHtml;
                                    questComplete = questComplete.Replace("<div id=\"lknlksndgg-completion\" style=\"display: none\">", "");

                                    questComplete = questComplete.Replace("</div>", "");
                                    if (issql)
                                        questComplete = questComplete.Replace("<br>", "$B"); // equivalent to new line in trinity database 
                                    else
                                        questComplete = questComplete.Replace("<br>", "NEW_LINE"); // equivalent to new line in wow database 

                                }
                            }
                            catch
                            {
                                goto SaidaDoLoop;
                            }

                        }
                        else
                        {

                            questTexts = document.DocumentNode.SelectSingleNode(htmlPart + "//h2[@class='heading-size-3']").NextSibling;


                            try
                            {
                                questProgress = document.DocumentNode.SelectSingleNode(htmlPart + "//div[@id='lknlksndgg-progress']").InnerText;
                                questComplete = document.DocumentNode.SelectSingleNode(htmlPart + "//div[@id='lknlksndgg-completion']").OuterHtml;
                                questComplete = questComplete.Replace("<div id=\"lknlksndgg-completion\" style=\"display: none\">", "");
                                questComplete = questComplete.Replace("</div>", "");
                                if (issql)
                                    questComplete = questComplete.Replace("<br>", "$B"); // equivalent to new line in trinity database 
                                else
                                    questComplete = questComplete.Replace("<br>", "NEW_LINE"); // equivalent to new line in wow database 
                            }
                            catch
                            {
                            }

                            List<HtmlNode> qDescription = new List<HtmlNode>();

                            while (questTexts != null && questTexts.Name != "h2")
                            {
                                qDescription.Add(questTexts);
                                questTexts = questTexts.NextSibling;
                            }

                            StringBuilder strBuilder = new StringBuilder();
                            for (int j = 0; j < qDescription.Count; j++)
                            {

                                if (qDescription[j].OuterHtml == "<br />")
                                {
                                    if (issql)
                                        strBuilder.Append("$B"); // equivalent to new line in trinity database
                                    else
                                        strBuilder.Append("NEW_LINE"); // equivalent to new line in trinity database
                                }
                                else
                                {
                                    strBuilder.Append(qDescription[j].OuterHtml);
                                }
                            }
                             
                            questText = strBuilder.ToString();
                        }



                        MakeSQL(Convert.ToInt32(beginId.Text), questName, questObjectives, questText, questProgress, questComplete, sqlFile);
                        
                        SaveFile();

                        SaidaDoLoop:
                        progressBar1.Value = progressBar1.Maximum;

                    }
                  catch { }
                }
                else
                {
                    MessageBox.Show("Object Id cannot be 0 (zero)");
                    sqlFile.Clear();
                }
            }
            else
            {

                if ((Convert.ToInt32(beginId.Text) >= 0) && (Convert.ToInt32(EndId.Text) > 0))
                {
                    //timer1.Start();
                    progressBar1.Maximum = Convert.ToInt32(EndId.Text) - Convert.ToInt32(beginId.Text); 

                    int[] firstThread;
                    int[] secondThread;
                    int[] thirdThread;
                    int[] forthThread;

                    int divided = ((Convert.ToInt32(EndId.Text)) - (Convert.ToInt32(beginId.Text)) / 4);
                    fstSizes = divided;
                    frthSize = divided + (((Convert.ToInt32(EndId.Text)) - (Convert.ToInt32(beginId.Text))) % 4);


                    if (divided > 3)
                    { 
                        firstThread = new int[fstSizes];
                        secondThread = new int[fstSizes];
                        thirdThread = new int[fstSizes];
                        forthThread = new int[frthSize];
                    }
                    else
                    {
                        frthSize = fstSizes;

                        firstThread = new int[divided];
                        secondThread = new int[divided];
                        thirdThread = new int[divided];
                        forthThread = new int[divided];
                    }

                    int assignedThread = 1;

                    int frstI = 0;
                    int scndI = 0;
                    int thrdI = 0;
                    int frthI = 0;

                    for (int i = Convert.ToInt32(beginId.Text); i <= Convert.ToInt32(EndId.Text); i++)
                    {
                        if(assignedThread == 1)
                        {
                            firstThread[frstI] = i;
                            frstI++;
                        } 
                        else if(assignedThread == 2)
                        {
                            secondThread[scndI] = i;
                            scndI++;
                        }
                        else if(assignedThread == 3)
                        {
                            thirdThread[thrdI] = i;
                            thrdI++;
                        }
                        else if(assignedThread == 4)
                        {
                            forthThread[frthI] = i;
                            frthI++;
                        }


                        assignedThread++;

                        if (assignedThread == 5)
                        {
                            assignedThread = 1;
                            if ((frstI > divided && scndI > divided) && thrdI > divided)
                                assignedThread = 4; 
                        }

                    }

                    timer1.Start();

                    new Thread(() => { BeginParse(firstThread, 1); }).Start();
                    new Thread(() => { BeginParse(secondThread, 2); }).Start();
                    new Thread(() => { BeginParse(thirdThread, 3); }).Start();
                    new Thread(() => { BeginParse(forthThread, 4); }).Start();
                }
                else
                {
                    MessageBox.Show("Last Id cannot be 0 (zero)");
                    sqlFile.Clear();
                }
            }
        }
        private void MergeThreadStrings()
        {
            sqlFile.AddRange(firstThreadStrings);
            sqlFile.AddRange(secondThreadStrings);
            sqlFile.AddRange(thirdThreadStrings);
            sqlFile.AddRange(forthThreadStrings);

            firstThreadStrings.Clear();
            secondThreadStrings.Clear();
            thirdThreadStrings.Clear();
            forthThreadStrings.Clear();
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
            try
            {
                progressBar1.Value = progress;
            }
            catch { }


            status.Text = progress.ToString() + "/" + progressBar1.Maximum;

            if (Truth(new bool[] { isSecondFinishd, isFirstFinishd, isThirdFinishd, isForthFinishd }) == 4)
            {
                progressBar1.Value = progressBar1.Maximum;
                timer1.Stop();

                MergeThreadStrings();
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

        private void questControl_Resize(object sender, EventArgs e)
        {
            status.Left = (this.ClientSize.Width - status.Width) / 2;
        }

        private void RItem_CheckedChanged(object sender, EventArgs e)
        {

            if (rItem.Checked == true)
            {
                Parse.Enabled = false;
                ofd = new OpenFileDialog();
                ofd.Filter = "Text files (*.txt) (*.rng) | *.txt; *.rng;";
                ofd.FileOk += (s, en) =>
                {
                    List<int> listIndexes = Array.ConvertAll(File.ReadAllLines(ofd.FileName), sd => Int32.Parse(sd)).ToList();

                    //timer1.Start();
                    progressBar1.Maximum = listIndexes.Count; 

                    for (int i = 0; i < listIndexes.Count; i++)
                        if (listIndexes[i] > maxItemValue)
                            maxItemValue = listIndexes[i];

                    int[] firstThread;
                    int[] secondThread;
                    int[] thirdThread;
                    int[] forthThread;

                    int divided = listIndexes.Count / 4;
                    fstSizes = divided;
                    frthSize = divided + listIndexes.Count % 4;


                    if (divided > 3)
                    {
                        firstThread = new int[fstSizes];
                        secondThread = new int[fstSizes];
                        thirdThread = new int[fstSizes];
                        forthThread = new int[frthSize];
                    }
                    else
                    {
                        frthSize = fstSizes;

                        firstThread = new int[divided];
                        secondThread = new int[divided];
                        thirdThread = new int[divided];
                        forthThread = new int[divided];
                    }

                    int assignedThread = 1;

                    int frstI = 0;
                    int scndI = 0;
                    int thrdI = 0;
                    int frthI = 0;

                    for (int i = 0; i < listIndexes.Count; i++)
                    {
                        if (assignedThread == 1)
                        {
                            firstThread[frstI] = listIndexes[i];
                            frstI++;
                        }
                        else if (assignedThread == 2)
                        {
                            secondThread[scndI] = listIndexes[i];
                            scndI++;
                        }
                        else if (assignedThread == 3)
                        {
                            thirdThread[thrdI] = listIndexes[i];
                            thrdI++;
                        }
                        else if (assignedThread == 4)
                        {
                            forthThread[frthI] = listIndexes[i];
                            frthI++;
                        }


                        assignedThread++;

                        if (assignedThread == 5)
                        {
                            assignedThread = 1;
                            if ((frstI >= divided && scndI >= divided) && thrdI >= divided)
                                assignedThread = 4;
                        }

                    }

                    timer1.Start();

                    new Thread(() => { BeginParse(firstThread, 1); }).Start();
                    new Thread(() => { BeginParse(secondThread, 2); }).Start();
                    new Thread(() => { BeginParse(thirdThread, 3); }).Start();
                    new Thread(() => { BeginParse(forthThread, 4); }).Start();
                };

                ofd.ShowDialog();
                
            }
        }
    }


    public class wQuest
    {
        public string[] knownStrings = new string[] { "name", "class", "race" };
        public string[] dbStrings = new string[] { "$n", "$c", "$r" }; // same representation of knownStrings, with same Ids,
        public string[] addOnStrings = new string[] { "YOUR_NAME", "YOUR_CLASS", "YOUR_RACE" }; // same representation of knownStrings, with same Ids,
        public string questName { get; set; }

        public string questObjectives { get; set; }
        public string questText { get; set; }
        public string questProgress { get; set; }
        public string questComplete { get; set; }

    }
}
