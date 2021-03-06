﻿
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RedRat.RaceTiming.Data.Model;

namespace RedRat.RaceTimingWinApp.ExtendedListView
{
    public class ResultListViewItem : ListViewItem
    {
        public ResultListViewItem( Result result )
        {
            Text = result.Position.ToString();
            SubItems.Add(result.Time.ToString(@"hh\:mm\:ss"));
            SubItems.Add(result.RaceNumber.ToString());
            if (result.RaceNumber == 0)
            {
                // Only allow deletion if no race number associated with result.
                SubItems.Add("X");
            }
            // ToDo: Improve. The colour disappears on mouse over.
            if ( result.DubiousResult != Result.DubiousResultEnum.None )
            {
                BackColor = Color.Salmon;
                foreach ( var subItem in SubItems.Cast<ListViewSubItem>() )
                {
                    subItem.BackColor = Color.Salmon;
                }
            }
        }
    }
}
