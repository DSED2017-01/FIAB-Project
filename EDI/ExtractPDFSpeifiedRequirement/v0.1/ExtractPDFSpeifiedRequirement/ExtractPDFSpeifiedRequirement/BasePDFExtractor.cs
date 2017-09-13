﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPDFSpeifiedRequirement
{
    public abstract class BasePDFExtractor 
    {
        protected int page_count = 0;


        protected readonly string[] _ignore_list = { "Import Health Standard" };
        protected string[] ignored_words = { "Import Health Standard", "2017", "Page", "KEY", "CITES" };

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool CheckForWord(string line, string[] word_list)
        {
            bool flag = false;
            foreach (string text in word_list)
                if (line.Contains(text))
                    return true;
            return flag;
        }
    }
}
