using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Model;
using PanGu;

namespace Common
{
    public class WebCommon
    {
        public static List<string> PanGuSplitWord(string msg)
        {
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(msg));

            ITermAttribute ita;
            bool hasNext = true;
            List<string> list = new List<string>();
            while (hasNext)
            {
                ita = tokenStream.GetAttribute<ITermAttribute>();
                list.Add(ita.Term);
                hasNext = tokenStream.IncrementToken();
            }

            analyzer.Close();
            return list;

            /*Token token;
            List<string> list = new List<string>();
            while ((token = tokenStream.) != null)
            {
                list.Add(token.TermText());
            }
            return list;*/
        }

        public static string CreateHightLight(string keywords, string Content)
        {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
                new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
            PanGu.HighLight.Highlighter highlighter =
                new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
                    new Segment());
            //设置每个摘要段的字符数
//            highlighter.FragmentSize = 150;
            //获取最匹配的摘要段
            return highlighter.GetBestFragment(keywords, Content);
        }

        public static UserInfo GetCurrentUser(string sessionId)
        {
            string jsonStr = MemcachedHelper.Get(sessionId).ToString();
            return SerializeHelper.toJson<UserInfo>(jsonStr);
        }
    }
}