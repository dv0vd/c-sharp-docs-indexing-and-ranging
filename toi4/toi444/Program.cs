using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace toi444
{
    class Program
    {
        static Regex stopSymbolsStart = new Regex("^(еще |него |сказать |а |ж |нее |со |без |же |ней |совсем |более |жизнь |нельзя |так |больше |за |нет |такой |будет |зачем |ни |там |будто |здесь |нибудь |тебя |бы |и |никогда |тем |был |из |ним |теперь |была |из-за |них |то |были |или |ничего |тогда |было |им |но |того |быть |иногда |ну |тоже |в |их |о |только |вам |к |об |том |вас |кажется |один |тот |вдруг |как |он |три |ведь |какая |она |тут |во |какой |они |ты |вот |когда |опять |у |впрочем |конечно |от |уж |все |которого |перед |уже |всегда |которые |по |хорошо |всего |кто |под |хоть |всех |куда |после |чего |всю |ли |потом |человек |вы |лучше |потому |чем |г |между |почти |через |где |меня |при |что |говорил |мне |про |чтоб |да |много |раз |чтобы |даже |может |разве |чуть |два |можно |с |эти |для |мой |сам |этого |до |моя |свое |этой |другой |мы |свою |этом |его |на |себе |этот |ее |над |себя |эту |ей |надо |сегодня |я |ему |наконец |сейчас |если |нас |сказал |есть |не |сказала )", RegexOptions.IgnoreCase);
        static Regex figures = new Regex("(\\d)");
        static Regex punctuationSymbols = new Regex("(\\W)");
        static Regex spaces = new Regex("(  )");
        static Regex stopSymbolsMiddle = new Regex("( еще | него | сказать | а | ж | нее | со | без | же | ней | совсем | более | жизнь | нельзя | так | больше | за | нет | такой | будет | зачем | ни | там | будто | здесь | нибудь | тебя | бы | и | никогда | тем | был | из | ним | теперь | была | изза | них | то | были | или | ничего | тогда | было | им | но | того | быть | иногда | ну | тоже | в | их | о | только | вам | к | об | том | вас | кажется | один | тот | вдруг | как | он | три | ведь | какая | она | тут | во | какой | они | ты | вот | когда | опять | у | впрочем | конечно | от | уж | все | которого | перед | уже | всегда | которые | по | хорошо | всего | кто | под | хоть | всех | куда | после | чего | всю | ли | потом | человек | вы | лучше | потому | чем | г | между | почти | через | где | меня | при | что | говорил | мне | про | чтоб | да | много | раз | чтобы | даже | может | разве | чуть | два | можно | с | эти | для | мой | сам | этого | до | моя | свое | этой | другой | мы | свою | этом | его | на | себе | этот | ее | над | себя | эту | ей | надо | сегодня | я | ему | наконец | сейчас | если | нас | сказал | есть | не | сказала )", RegexOptions.IgnoreCase);
        static Regex PERFECTIVEGERUND = new Regex("(ав|авши|авшись|яв|явши|явшись|ив|ивши|ившись|ыв|ывши|ывшись)$");
        static Regex REFLEXIVE = new Regex("(ся|сь)$");
        static Regex NOUN = new Regex("(а|ев|ов|ие|ье|е|иями|ями|ами|еи|ии|и|ией|ей|ой|ий|й|иям|ям|ием|ем|ам|ом|о|у|ах|иях|ях|ы|ь|ию|ью|ю|ия|ья|я)$");
        static Regex VERB = new Regex("(ила|ла|ена|ейте|уйте|ите|или|ыли|ей|уй|ил|ыл|им|ым|ен|ило|ыло|ено|ят|ует|уют|ит|ыт|ены|ить|ыть|ишь|ую|ю|ала|ана|аете|айте|али|ай|ал|аем|ан|ало|ано|ает|ают|аны|ать|аешь|анно|яла|яна|яете|яйте|яли|яй|ял|яем|ян|яло|яно|яет|яют|яны|ять|яешь|янно)$");
        static Regex ADJECTIVAL = new Regex("(ее|ие|ые|ое|ими|ыми|ей|ий|ый|ой|ем|им|ым|ом|его|ого|ему|ому|их|ых|ую|юю|ая|яя|ою|ею|ившее|ившие|ившые|ившое|ившими|ившыми|ившей|ивший|ившый|ившой|ившем|ившим|ившым|ившом|ившего|ившого|ившему|ившому|ивших|ившых|ившую|ившюю|ившая|ившяя|ившою|ившею|ывшее|ывшие|ывшые|ывшое|ывшими|ывшыми|ывшей|ывший|ывшый|ывшой|ывшем|ывшим|ывшым|ывшом|ывшего|ывшого|ывшему|ывшому|ывших|ывшых|ывшую|ывшюю|ывшая|ывшяя|ывшою|ывшею|ующее|ующие|ующые|ующое|ующими|ующыми|ующей|ующий|ующый|ующой|ующем|ующим|ующым|ующом|ующего|ующого|ующему|ующому|ующих|ующых|ующую|ующюю|ующая|ующяя|ующою|ующею|аемее|аемие|аемые|аемое|аемими|аемыми|аемей|аемий|аемый|аемой|аемем|аемим|аемым|аемом|аемего|аемого|аемему|аемому|аемих|аемых|аемую|аемюю|аемая|аемяя|аемою|аемею|аннее|анние|анные|анное|анними|анными|анней|анний|анный|анной|аннем|анним|анным|анном|аннего|анного|аннему|анному|анних|анных|анную|аннюю|анная|анняя|анною|аннею|авшее|авшие|авшые|авшое|авшими|авшыми|авшей|авший|авшый|авшой|авшем|авшим|авшым|авшом|авшего|авшого|авшему|авшому|авших|авшых|авшую|авшюю|авшая|авшяя|авшою|авшею|ающее|ающие|ающые|ающое|ающими|ающыми|ающей|ающий|ающый|ающой|ающем|ающим|ающым|ающом|ающего|ающого|ающему|ающому|ающих|ающых|ающую|ающюю|ающая|ающяя|ающою|ающею|ащее|ащие|ащые|ащое|ащими|ащыми|ащей|ащий|ащый|ащой|ащем|ащим|ащым|ащом|ащего|ащого|ащему|ащому|ащих|ащых|ащую|ащюю|ащая|ащяя|ащою|ащею|яемее|яемие|яемые|яемое|яемими|яемыми|яемей|яемий|яемый|яемой|яемем|яемим|яемым|яемом|яемего|яемого|яемему|яемому|яемих|яемых|яемую|яемюю|яемая|яемяя|яемою|яемею|яннее|янние|янные|янное|янними|янными|янней|янний|янный|янной|яннем|янним|янным|янном|яннего|янного|яннему|янному|янних|янных|янную|яннюю|янная|янняя|янною|яннею|явшее|явшие|явшые|явшое|явшими|явшыми|явшей|явший|явшый|явшой|явшем|явшим|явшым|явшом|явшего|явшого|явшему|явшому|явших|явшых|явшую|явшюю|явшая|явшяя|явшою|явшею|яющее|яющие|яющые|яющое|яющими|яющыми|яющей|яющий|яющый|яющой|яющем|яющим|яющым|яющом|яющего|яющого|яющему|яющому|яющих|яющых|яющую|яющюю|яющая|яющяя|яющою|яющею|ящее|ящие|ящые|ящое|ящими|ящыми|ящей|ящий|ящый|ящой|ящем|ящим|ящым|ящом|ящего|ящого|ящему|ящому|ящих|ящых|ящую|ящюю|ящая|ящяя|ящою|ящею)$");
        static Regex DERIVATIONAL = new Regex("(ост|ость)$");
        static Regex SUPERLATIVE = new Regex("(ейш|ейше)$");
        static Regex NN = new Regex("(нн)$");
        static Regex SOFTSIGN = new Regex("(ь)$");
        static Regex AND = new Regex("(и)$");
        static char[] vowels = { 'а', 'о', 'у', 'ы', 'э', 'я', 'е', 'ю', 'и', 'А', 'О', 'У', 'Ы', 'Э', 'Я', 'Е', 'Ю', 'И' };
        static char[] consonants = { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ь' };

        static void Input( Model1 db)
        {
            Console.Write("Количество документов:   ");
            string temp = Console.ReadLine();
            int docCount = int.Parse(temp);
            for (int i=0; i<docCount; )
            {
                Console.Write("Имя файла документа:   ");
                string inputFileName = Console.ReadLine();
                FileInfo fileInf = new FileInfo(inputFileName);
                if (!fileInf.Exists)
                {
                    Console.WriteLine("Ошибка. Неверное имя!");
                    Console.WriteLine("************************************");
                }
                else
                {
                    i++;
                    db.Documents.Add(new Document { Id = i, Name = inputFileName });
                }
            }
            db.SaveChanges();
        }

        static void RemovingStopSymbols(Model1 db, ref string search)
        {
            if (search=="")
            {
                foreach (Document d in db.Documents)
                {
                    using (StreamReader headings = new StreamReader(d.Name))
                    {
                        string newName = "D:\\" + (d.Id).ToString() + (d.Id).ToString() + ".txt";
                        using (StreamWriter newHeadings = new StreamWriter(newName))
                        {
                            string line;
                            while ((line = headings.ReadLine()) != null)
                            {
                                line = stopSymbolsStart.Replace(line, "");
                                line = figures.Replace(line, "");
                                line = punctuationSymbols.Replace(line, " ");
                                line = stopSymbolsMiddle.Replace(line, " ");
                                line = spaces.Replace(line, " ");
                                newHeadings.WriteLine(line);
                            }
                        }
                    }
                }
            }
            else
            {
                search = stopSymbolsStart.Replace(search, "");
                search = figures.Replace(search, "");
                search = punctuationSymbols.Replace(search, " ");
                search = stopSymbolsMiddle.Replace(search, " ");
                search = spaces.Replace(search, " ");
            }
        }

        static string RVFinding(string word)
        {
            int position = -1;
            bool check = false;
            for (int i = 0; i < word.Length; i++)
            {
                int j = 0;
                foreach (char ch in vowels)
                {
                    if (word[i] == vowels[j])
                    {
                        position = i;
                        check = true;
                        break;
                    }
                    j++;
                }
                if (check)
                    break;
            }
            if ((position == -1) || (position == word.Length - 1))
                return "";
            else
            {
                string rv = "";
                for (int i = position + 1; i < word.Length; i++)
                    rv += word[i];
                return rv;
            }
        }

        static string R1Finding(string word)
        {
            string r1 = "";
            bool check = false;
            int pos = -1;
            if (word.Length < 2)
                return "";
            for (int i = 0; i < word.Length - 1; i++)
            {
                int f = 0;
                foreach (char c in vowels)
                {
                    if (word[i] == vowels[f])
                    {
                        check = true;
                        break;
                    }
                    f++;
                }
                if (check)
                {
                    f = 0;
                    foreach (char c in consonants)
                    {
                        if (word[i + 1] == consonants[f])
                        {
                            pos = i + 1;
                            break;
                        }
                        f++;
                    }
                }
                if (pos != -1)
                    break;
            }
            if (pos == -1)
                return "";
            for (int i = pos + 1; i < word.Length; i++)
                r1 += word[i];
            return r1;
        }

        static void Step1(string rv, ref string word)
        {
            if (PERFECTIVEGERUND.IsMatch(rv))
            {
                word = PERFECTIVEGERUND.Replace(word, "");
            }
            else
            {
                word = REFLEXIVE.Replace(word, "");
                if (ADJECTIVAL.IsMatch(rv))
                {
                    word = ADJECTIVAL.Replace(word, "");
                }
                else
                {
                    if (VERB.IsMatch(rv))
                    {
                        word = VERB.Replace(word, "");
                    }
                    else
                    {
                        if (NOUN.IsMatch(rv))
                        {
                            word = NOUN.Replace(word, "");
                        }
                    }
                }
            }
        }

        static void Step2(string rv, ref string word)
        {
            word = AND.Replace(word, "");
        }

        static void Step3(ref string word, string r2)
        {
            if (DERIVATIONAL.IsMatch(r2))
            {
                word = DERIVATIONAL.Replace(word, "");
            }
        }

        static void Step4(string rv, ref string word)
        {
            if (NN.IsMatch(rv))
            {
                word = NN.Replace(word, "н");
            }
            else
            {
                if (SUPERLATIVE.IsMatch(rv))
                {
                    word = SUPERLATIVE.Replace(word, "");
                    if (NN.IsMatch(rv))
                    {
                        word = NN.Replace(word, "н");
                    }
                }
                else
                {
                    if (SOFTSIGN.IsMatch(rv))
                    {
                        word = SOFTSIGN.Replace(word, "");
                    }
                }
            }
        }

        static void Stemming(Model1 db, ref string search, ref string newSearch)
        {
            if(search=="")
            {
                foreach (Document d in db.Documents)
                {
                    using (StreamReader newHeadings = new StreamReader("D:\\" + d.Id.ToString() + d.Id.ToString() + ".txt"))
                    {
                        using (StreamWriter newHeadingsAfterStemming = new StreamWriter("D:\\" + d.Id.ToString() + d.Id.ToString() + d.Id.ToString() + ".txt"))
                        {
                            string line;
                            while ((line = newHeadings.ReadLine()) != null)
                            {
                                int i = 0;
                                string word = "";
                                string rv, r1, r2;
                                line += " ";
                                while (i < line.Length)
                                {
                                    if (line[i] != ' ')
                                    {
                                        word += line[i];
                                    }
                                    else
                                    {
                                        rv = RVFinding(word);
                                        if (rv != "")
                                        {
                                            r1 = R1Finding(word);
                                            r2 = R1Finding(r1);
                                            Step1(rv, ref word);
                                            Step2(rv, ref word);
                                            Step3(ref word, r2);
                                            Step4(rv, ref word);
                                        }
                                        newHeadingsAfterStemming.Write(word + " ");
                                        word = "";
                                    }
                                    i++;
                                }
                                newHeadingsAfterStemming.WriteLine("");
                            }
                        }
                    }
                }
            }
            else
            {
                int i = 0;
                string word = "";
                string rv, r1, r2;
                search += " ";
                while (i < search.Length)
                {
                    if (search[i] != ' ')
                    {
                        word += search[i];
                    }
                    else
                    {
                        rv = RVFinding(word);
                        if (rv != "")
                        {
                            r1 = R1Finding(word);
                            r2 = R1Finding(r1);
                            Step1(rv, ref word);
                            Step2(rv, ref word);
                            Step3(ref word, r2);
                            Step4(rv, ref word);
                        }
                        newSearch += word + " ";
                        word = "";
                    }
                    i++;
                }
            }
        }

        static void LoadWordsDB(Model1 db)
        {
            int count = 0;
            ArrayList docsId = new ArrayList();
            foreach (Document d in db.Documents)
            {
                docsId.Add(d.Id);
            }
            for (int j = 0; j < docsId.Count; j++)
            {
                using (StreamReader newHeadings = new StreamReader("D:\\" + docsId[j].ToString() + docsId[j].ToString() + docsId[j].ToString() + ".txt"))
                {
                    string line;
                    while ((line = newHeadings.ReadLine()) != null)
                    {
                        line += " ";
                        int i = 0;
                        string word = "";
                        while (i < line.Length)
                        {
                            if (line[i] != ' ')
                            {
                                word += line[i];
                            }
                            else
                            {
                                if (word != "")
                                {
                                    word = word.ToLower();
                                    bool check = false;
                                    foreach (Word w in db.Words)
                                    {
                                        if (word == w.word1)
                                        {
                                            check = true;
                                            break;
                                        }
                                    }
                                    if (!check)
                                    {
                                        count++;
                                        db.Words.Add(new Word { Id = count, word1 = word });
                                        db.SaveChanges();
                                    }
                                    word = "";
                                }
                            }
                            i++;
                        }
                    }
                }
            }
            using (StreamWriter newHeadingsAfterStemming = new StreamWriter("D:\\WORDS.txt"))
            {
                foreach (Word w in db.Words)
                {
                    newHeadingsAfterStemming.WriteLine("{0} - {1}", w.Id, w.word1);
                }
            }
        }

        static void LoadInverseDB(Model1 db)
        {
            ArrayList docsId = new ArrayList();
            foreach (Document d in db.Documents)
            {
                docsId.Add(d.Id);
            }
            ArrayList wordsName = new ArrayList();
            ArrayList wordsId = new ArrayList();

            foreach (Word w in db.Words)
            {
                wordsName.Add(w.word1);
                wordsId.Add(w.Id);
            }
            for(int k =0; k<wordsId.Count; k++)
            {
                string docsReferences = "";

                for (int j = 0; j < docsId.Count; j++)
                {
                    using (StreamReader newHeadings = new StreamReader("D:\\" + docsId[j].ToString() + docsId[j].ToString() + docsId[j].ToString() + ".txt"))
                    {
                        string line;
                        while ((line = newHeadings.ReadLine()) != null)
                        {
                            line += " ";
                            int i = 0;
                            string word = "";
                            while (i < line.Length)
                            {
                                if (line[i] != ' ')
                                {
                                    word += line[i];
                                }
                                else
                                {
                                    if (word != "")
                                    {
                                        word = word.ToLower();
                                        if(word == (string)wordsName[k])
                                        {
                                            string subString = docsId[j].ToString() + " ";
                                            int indexOfSubstring = docsReferences.IndexOf(subString);
                                            if(indexOfSubstring == -1)
                                            {
                                                docsReferences += docsId[j].ToString() + " ";
                                            }
                                        }
                                        word = "";
                                    }
                                }
                                i++;
                            }
                        }
                    }
                }
                db.Inverses.Add(new Inverse { Id = k+1, wordId = k+1, docs = docsReferences });
                db.SaveChanges();
                using (StreamWriter newHeadingsAfterStemming = new StreamWriter("D:\\INVERSES.txt"))
                {
                    foreach (Inverse w in db.Inverses)
                    {
                        newHeadingsAfterStemming.WriteLine("{0} - {1}", w.wordId, w.docs);
                    }
                }
            }
        }

        static string SearchPhrase()
        {
            string searchRequest = "";
            Console.WriteLine("**********************************************");
            Console.WriteLine("//////////////////////////////////////////////");
            Console.Write("Поиск:   ");
            searchRequest = Console.ReadLine();
            return searchRequest;
        }

        static int FindWordId(Model1 db, string word)
        {
            foreach(Word w in db.Words)
            {
                if (w.word1 == word)
                    return w.Id;
            }
            return -1;
        }

        static void Ranking(Model1 db, string search, double[] scoreMas, HashSet<int> documents)
        {
            //wphrase
            foreach(int i in documents)
            {
                double wphrase = 0;
                using (StreamReader newHeadings = new StreamReader("D:\\" + i.ToString()+ i.ToString() + i.ToString() +  ".txt"))
                {
                    string line;
                    while ((line = newHeadings.ReadLine()) != null)
                    {
                        line = line.ToLower();
                        int indexOfSubstring = line.IndexOf(search);
                        if (indexOfSubstring != -1)
                        {
                            wphrase++;
                        }
                    }
                    scoreMas[i - 1] += wphrase*100 ;
                }
            }
            //end wphrase
            //wsingle
            foreach (int i in documents)
            {
                int f = 0;
                string word = "";
                while (f < search.Length)
                {
                    if (search[f] != ' ')
                    {
                        word += search[f];
                    }
                    else
                    {
                        using (StreamReader newHeadings = new StreamReader("D:\\" + i.ToString() + i.ToString() + i.ToString() + ".txt"))
                        {
                            if (word != "")
                            {
                                word = word.ToLower();
                                string line;
                                while ((line = newHeadings.ReadLine()) != null)
                                {
                                    line += " ";
                                    line = line.ToLower();
                                    int indexOfSubstring = line.IndexOf(word);
                                    if (indexOfSubstring != -1)
                                    {
                                        scoreMas[i - 1]++;
                                    }
                                }
                                word = "";
                            }
                        }
                    }   
                    f++;
                }
            }
            //end wsingle
            //wallwords
            foreach (int i in documents)
            {
                int check = 1;
                int f = 0;
                string word = "";
                while (f < search.Length)
                {
                    if (search[f] != ' ')
                    {
                        word += search[f];
                    }
                    else
                    {
                        using (StreamReader newHeadings = new StreamReader("D:\\" + i.ToString() + i.ToString() + i.ToString() + ".txt"))
                        {
                            if (word != "")
                            {
                                word = word.ToLower();
                                string line;
                                bool checking = false;
                                while ((line = newHeadings.ReadLine()) != null)
                                {
                                    line += " ";
                                    line = line.ToLower();
                                    int indexOfSubstring = line.IndexOf(word);
                                    if (indexOfSubstring != -1)
                                    {
                                        checking = true;
                                        break;
                                    }
                                }
                                if (!checking)
                                {
                                    check = 0;
                                    f = search.Length + 1;
                                }
                                word = "";
                            }
                        }
                    }
                    f++;
                }
                if (check != 0)
                    scoreMas[i - 1] += 50;
            }
            //end wallwords
            //wpair
            foreach (int i in documents)
            {
                int f = 0;
                string word = "";
                int check = 0;
                int pos=0;
                while (f < search.Length)
                {
                    if ((search[f] != ' '))
                    {
                        word += search[f];
                    }
                    else
                    {
                        check++;
                        if (check == 1)
                            pos = f;
                        if(check == 2)
                        {
                            using (StreamReader newHeadings = new StreamReader("D:\\" + i.ToString() + i.ToString() + i.ToString() + ".txt"))
                            {
                                if (word != "")
                                {
                                    word = word.ToLower();
                                    string line;
                                    while ((line = newHeadings.ReadLine()) != null)
                                    {
                                        line += " ";
                                        line = line.ToLower();
                                        int indexOfSubstring = line.IndexOf(word);
                                        if (indexOfSubstring != -1)
                                        {
                                            scoreMas[i - 1]+=25;
                                        }
                                    }
                                    word = "";
                                }
                            }
                            check = 0;
                            f = pos ;
                        }
                        else
                            word += search[f];
                    }
                    f++;
                }
            }
            //end wpair
        }

        static HashSet<int> FindAppropriateDocs(Model1 db, string search)
        {
            int i = 0;
            int wordsCount = 0;
            string word = "";
            int[] setofwords = { };
            while (i < search.Length)
            {
                if (search[i] != ' ')
                {
                    word += search[i];
                }
                else
                {
                    if (word != "")
                    {
                        wordsCount++;
                        word = "";
                    }
                }
                i++;
            }
            HashSet<int>[] frequency = new HashSet<int>[wordsCount];

            for (int j = 0; j < wordsCount; j++)
            {
                frequency[j] = new HashSet<int>();
            }
            wordsCount = 0;
            i = 0;
            while (i < search.Length)
            {
                if (search[i] != ' ')
                {
                    word += search[i];
                }
                else
                {
                    if (word != "")
                    {
                        word = word.ToLower();
                        int wordId = FindWordId(db, word);
                        if (wordId != -1)
                        {
                            string docs = "";
                            foreach (Inverse im in db.Inverses)
                            {
                                if (wordId == im.wordId)
                                {
                                    docs = im.docs;
                                    break;
                                }
                            }
                            int d = 0;
                            string wordDoc = "";
                            while (d < docs.Length)
                            {
                                if (docs[d] != ' ')
                                {
                                    wordDoc += docs[d];
                                }
                                else
                                {
                                    int id = Int32.Parse(wordDoc);
                                    frequency[wordsCount].Add(id);
                                    wordDoc = "";
                                }
                                d++;
                            }
                        }
                        word = "";
                        wordsCount++;
                    }
                }
                i++;
            }
            HashSet<int> result = new HashSet<int>();
            result = frequency[0];
            for (int j = 1; j < wordsCount; j++)
            {
                result.IntersectWith(frequency[j]);
            }
            return result;
        }

        static int GetMaxId(double[] scoreMas, ref int score)
        {
            //for (int i = 0; i < scoreMas.Length; i++)
            //{
            //    for (int j = i + 1; j < scoreMas.Length; j++)
            //    {
            //        if (scoreMas[j] < scoreMas[i])
            //        {
            //            var temp = scoreMas[i];
            //            scoreMas[i] = scoreMas[j];
            //            scoreMas[j] = temp;
            //        }
            //    }
            //}
            double max = scoreMas[0];
            int id=0;
            for (int i = 1; i < scoreMas.Length; i++)
            {
                if (scoreMas[i] > max)
                {
                    max = scoreMas[i];
                    id = i;
                }
            }
            score = (int)scoreMas[id];
            scoreMas[id] = 0;
            return id;
        }


        static void Output(Model1 db, HashSet<int> result, double[] scoreMas)
        {
            if(result.Count == 0)
            {
                Console.WriteLine("Упс... Поиск не дал результатов");
            }
            else
            {
                for(int i = 0; i < result.Count; i++)
                {
                    int score=0;
                    int id = GetMaxId(scoreMas, ref score);
                    id++;
                    Console.WriteLine("-------------------------------------------------------------");
                    string nameDoc = "";
                    foreach (Document d in db.Documents)
                    {
                        if (id == d.Id)
                        {
                            nameDoc = d.Name;
                            break;
                        }
                    }


                    Console.WriteLine(score.ToString());
                    Console.WriteLine(nameDoc);

                    //using (StreamReader newHeadings = new StreamReader(nameDoc))
                    //{
                    //    Console.WriteLine(score.ToString());
                    //    string line = newHeadings.ReadLine();
                    //    line = line.ToUpper();
                    //    Console.WriteLine(line);
                    //    line = "        ";
                    //    line += newHeadings.ReadLine();
                    //    Console.WriteLine(line);
                    //}
                }
            }
        }

        static void Main(string[] args)
        {
            using (Model1 db = new Model1())
            {
                Input(db);
                string searchRequest = "";
                RemovingStopSymbols(db,ref searchRequest);
                Stemming(db, ref searchRequest, ref searchRequest);
                LoadWordsDB(db);
                LoadInverseDB(db);
                while(true)
                {
                    searchRequest = SearchPhrase();
                    RemovingStopSymbols(db, ref searchRequest);
                    string searchRequestAfterStemming = "";
                    Stemming(db, ref searchRequest, ref searchRequestAfterStemming);
                    searchRequestAfterStemming = spaces.Replace(searchRequestAfterStemming, " ");
                    HashSet<int> docs = FindAppropriateDocs(db, searchRequestAfterStemming);
                    int docCount =db.Documents.Count();
                    double[] scoreMas = new double[docCount];
                    for (int i = 0; i < docCount; i++)
                        scoreMas[i] = 0;
                    if (docs.Count != 0)
                    {
                        Ranking(db, searchRequestAfterStemming, scoreMas, docs);
                    }
                    Output(db, docs, scoreMas);
                }
            }
        }
    }
}
