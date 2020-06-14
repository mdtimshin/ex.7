using System;
using System.Collections.Generic;
using System.Linq;

namespace ex._7
{
    class Program
    {
        public struct huffmannTreeNode
        {
            /// <summary>
            /// Текст
            /// </summary>
            public String text;
            /// <summary>
            /// Двоичный код
            /// </summary>
            public String code;
            /// <summary>
            /// Частота встречаемости
            /// </summary>
            public int frequency;

            public huffmannTreeNode(String t, String c, int f)
            {
                text = t;
                code = c;
                frequency = f;
            }
        }

        /// <summary>
        /// Частота встречаемости отдельных символов алфавита
        /// </summary>
        static Dictionary<char, int> freqs = new Dictionary<char, int>();
        /// <summary>
        /// Исходное дерево
        /// </summary>
        static List<huffmannTreeNode> source = new List<huffmannTreeNode>();
        /// <summary>
        /// Вспомогательное дерево
        /// </summary>
        static List<huffmannTreeNode> newRes = new List<huffmannTreeNode>();
        /// <summary>
        /// Еще какое-то дерево
        /// </summary>
        static List<huffmannTreeNode> tree = new List<huffmannTreeNode>();


        /// <summary>
        /// Сортировка узлов дерева по убыванию
        /// </summary>
        static void sortTree()
        {
            for (int index = 0; index < tree.Count - 1; index++)
            {
                for (int index2 = index; index2 < tree.Count; index2++)
                {
                    if (tree[index].frequency < tree[index2].frequency)
                    {
                        huffmannTreeNode buf = tree[index];
                        tree[index] = tree[index2];
                        tree[index2] = buf;
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество символов во входном алфавите");
            int kolSymbol = int.Parse(Console.ReadLine());
            for (int i = 0; i < kolSymbol; i++)
            {
                Console.WriteLine("Введите символ");
                char symbol = char.Parse(Console.ReadLine());
                Console.WriteLine("Введите частоту");
                int frequency = int.Parse(Console.ReadLine());
                freqs.Add(symbol, frequency);
            }
            foreach (KeyValuePair<char, int> Pair in freqs)
            {
                source.Add(new huffmannTreeNode(Pair.Key.ToString(), "", Pair.Value));
                tree.Add(new huffmannTreeNode(Pair.Key.ToString(), "", Pair.Value));
                newRes.Add(new huffmannTreeNode(Pair.Key.ToString(), "", Pair.Value));
            }

            while (tree.Count > 1)
            {
                sortTree();

                for (int index = 0; index < source.Count; index++)
                {
                    if (tree[tree.Count - 2].text.Contains(source[index].text))
                    {
                        newRes[index] = new huffmannTreeNode(newRes[index].text, "0" + newRes[index].code, newRes[index].frequency);
                    }
                    else if (tree[tree.Count - 1].text.Contains(source[index].text))
                    {
                        newRes[index] = new huffmannTreeNode(newRes[index].text, "1" + newRes[index].code, newRes[index].frequency);
                    }
                }

                tree[tree.Count - 2] = new huffmannTreeNode(tree[tree.Count - 2].text + tree[tree.Count - 1].text, "",
                    tree[tree.Count - 2].frequency + tree[tree.Count - 1].frequency);
                tree.RemoveAt(tree.Count - 1);
            }
            SortedDictionary<string, string> codes = new SortedDictionary<string, string>();
            for (int index = 0; index < source.Count; index++)
            {
                codes.Add(newRes[index].code, newRes[index].text);
            }
            foreach (KeyValuePair<string, string> keyValue in codes)
            {
                Console.WriteLine(keyValue.Value + " (" + keyValue.Key + ")");
            }

            Console.ReadKey();


            
        }
        
    }
}
