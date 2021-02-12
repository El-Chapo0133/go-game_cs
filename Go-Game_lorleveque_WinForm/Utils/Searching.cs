/**
 * Author : Loris Levêque
 * Date : 04.02.2021
 * Description : Provide functions to search something in lists or arrays
 * *****************************************************/


using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Go_Game_lorleveque_WinForm.Utils
{
    class Searching
    {
        public Panel searchPanelByName(object[] objects, String name)
        {
            var allPanels = searchObjectByType<Panel>(objects); // IEnumerable<object> where object is a Panel
            var result = from panel in allPanels
                         where ((Panel)panel).Name == name
                         select panel;
            return (Panel)result.First();
        }
        public IEnumerable<object> searchObjectByType<T>(object[] objects) where T: Control
        {
            return from tempObject in objects
                   where tempObject.GetType() == typeof(T)
                   select tempObject;
        }
    }
}
