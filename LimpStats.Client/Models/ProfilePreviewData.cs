﻿using System.Collections.Generic;
using System.Linq;
using LimpStats.Core;
using LimpStats.Model;

namespace LimpStats.Client.Models
{
    public class ProfilePreviewData
    {
        public string Username { get; }
        public int Points { get; }
        public ProfilePreviewData(string username, int points)
        {
            Username = username;
            Points = points;
        }

        public override string ToString()
        {
            return $"{Username} [{Points}]";
        }

        public static IEnumerable<ProfilePreviewData> GetProfilePreview(StudyGroup group)
        {
            return group
                .GetTotalPoints()
                .Select(res => new ProfilePreviewData(res.Username, res.Points));
        }
        public static IEnumerable<ProfilePreviewData> GetProfilePackPreview(StudyGroup group, string packtitle)
        {
            var pack =   group.ProblemPackList.Find(e => e.PackTitle == packtitle);
            return group
                   .GetPackResult(pack)
                   .Select(res => new ProfilePreviewData(res.Username, res.TotalPoints));
        }
    }
}