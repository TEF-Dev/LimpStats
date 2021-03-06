﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;
using LimpStats.Model;
using LimpStats.Model.Problems;
using System.IO;
using OfficeOpenXml;
using Microsoft.Win32;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    public partial class ProblemTasksPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;

        private readonly UserGroup _group;


        public ProblemTasksPreview(UserGroup users, string packTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            InitializeComponent();


            _group = users;
            PackTitle = packTitle;
            CardTitle.DataContext = packTitle;

            if (_group.ProblemsPacks.Capacity != 0)
            {
                var studentsData = ProfilePreviewData.GetProfilePackPreview(_group, packTitle);

                foreach (var currRes in studentsData)
                {
                    ThreadingTools.ExecuteUiThread(() =>
                        Panel.Children.Add(new UserResPrewiew(currRes.Username, currRes.Points)));
                }
            }// ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);

        }

        public string PackTitle { get; }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            if (Core.Tools.Tools.CheckInternetConnect() == false)
            {
                MessageBox.Show("Internet connection error");
                return;
            }

            Task.Run(() => Update());

        }

        public void Update()
        {
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = false);

            MultiThreadParser.LoadProfiles(_group);

            IEnumerable<ProfilePreviewData> studentsData = ProfilePreviewData.GetProfilePackPreview(_group, PackTitle);

            ThreadingTools.ExecuteUiThread(() => Panel.Children.Clear());

            foreach (var currRes in studentsData)
            {
                ThreadingTools.ExecuteUiThread(() => Panel.Children.Add(new UserResPrewiew(currRes.Username, currRes.Points)));
            }
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = true);
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            string packTitle = CardTitle.DataContext.ToString();
            ProblemsPack pack = _group.ProblemsPacks.Find(p => p.Title == packTitle);
            var resultGridBlock = new ResultGridBlock(_group.Users, pack);

            _navigateService.AddToViewList(CardTitle.DataContext.ToString(), resultGridBlock);
            _navigateService.OpenView(resultGridBlock);
            
        }

        private void SaveToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            var studentsData = ProfilePreviewData.GetProfilePackPreview(_group, PackTitle);
           
            using (var excel = new ExcelPackage(new FileInfo(SaveFileDialog() + ".xlsx")))
            {
                var ws = excel.Workbook.Worksheets.Add("StudentGroup");
                int i = 1;
                foreach (var curRes in studentsData)
                {
                    ws.Cells[i, 1].Value = curRes.Username;
                    ws.Cells[i, 2].Value = curRes.Points;
                    i++;
                }
                excel.Save();
            }
        }

        //TODO: Duplicate
        private static string SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }
    }
}
