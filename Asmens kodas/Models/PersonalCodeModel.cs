﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asmens_kodas.Models
{
    public class PersonalCodeModel
    {
        public long Code { get; set; }
        public PersonalCodeModel(DateTime date, GenderEnum gender, int lineNumber) 
        {
            CreateCode(date, gender, lineNumber);
        }

        private void CreateCode(DateTime date, GenderEnum gender, int lineNumber)
        {
            Code = GetGenderDidget(date.Year, gender);
            Code *= 1000000;
            Code += GetBithDateDidgets(date);
            Code *= 1000;
            Code += lineNumber;
            Code *= 10;
            Code += GetLastDidget(Code);
        }

        private int GetGenderDidget(int year, GenderEnum gender)
        {
            int genderNum;
            if(gender == GenderEnum.Male)
            {
                genderNum = 1;
            }else
            {
                genderNum = 2;
            }

            //century - (minCentury-19)*2
            int uniqueGenderAdder = (GetCentury(year) - 19) * 2;

            return genderNum + uniqueGenderAdder;
        }

        private int GetBithDateDidgets(DateTime date)
        {
            string dateCode = "";
            dateCode += (date.Year.ToString()).Substring(2,2);

            //get month ( if less than 10 adds 0)
            if(date.Month < 10)
            {
                dateCode += '0' + date.Month.ToString();
            }else
            {
                dateCode += date.Month;
            }

            //get day (if less than 10 adds 0)
            if(date.Day < 10)
            {
                dateCode += '0' + date.Day.ToString();
            }else
            {
                dateCode += date.Day;
            }

            return int.Parse(dateCode);
        }

        private int GetLastDidget(long currentCode)
        {
            string code = currentCode.ToString();
            List<int> codeNumbers = new List<int>();
            foreach(char number in code)
            {
                codeNumbers.Add(int.Parse(number.ToString()));
            }

            int lastDidget = (codeNumbers[0] * 1) + (codeNumbers[1] * 2) + (codeNumbers[2] * 3) + (codeNumbers[3] * 4) + (codeNumbers[4] * 5) + (codeNumbers[5] * 6) + (codeNumbers[6] * 7) + (codeNumbers[7] * 8) + (codeNumbers[8] * 9) + (codeNumbers[9] * 1);
            lastDidget = lastDidget % 11;
            if(lastDidget >= 10)
            {
                lastDidget = (codeNumbers[0] * 3) + (codeNumbers[1] * 4) + (codeNumbers[2] * 5) + (codeNumbers[3] * 6) + (codeNumbers[4] * 7) + (codeNumbers[5] * 8) + (codeNumbers[6] * 9) + (codeNumbers[7] * 1) + (codeNumbers[8] * 2) + (codeNumbers[9] * 3);
                lastDidget = lastDidget % 11;
                if(lastDidget >= 10)
                {
                    return 0;
                }else
                {
                    return lastDidget;
                }
            }
            else
            {
                return lastDidget;
            }
        }

        private int GetCentury(int year)
        {
            return (int)Math.Round((double)(year / 100), 0) + 1;
        }
    }
}
