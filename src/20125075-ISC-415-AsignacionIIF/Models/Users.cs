﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Models
{
    public class Users
    {
        private static Users uniqueInstance = null;
        
        public Dictionary<String, DateTime> userList = new Dictionary<String, DateTime>();
        public Dictionary<Tuple<String, String>, List<olddMessage>> userMessages = new Dictionary<Tuple<String, String>, List<olddMessage>>();
        public Dictionary<String, String> userImages = new Dictionary<String, String>();

       private Users()
        {

        }

        public static Users getUniqueInstance()
        {
            if (null == uniqueInstance)
                uniqueInstance = new Users();
            return uniqueInstance;
        }
    }
}
