﻿using System.Collections.Generic;
using ElimpParse.Model;

namespace ElimpParse.DatabaseProvider
{
    public class DataGenerator
    {
        public static StudyGroup GenerateTemplateGroup()
        {
            var users = new List<ElimpUser>
            {
                new ElimpUser("Andrey2005"),
                new ElimpUser("DDsov"),
                //new ElimpUser("Den4758"),
                new ElimpUser("Gladtoseeyou"),
                new ElimpUser("Koteika"),
                new ElimpUser("liza.898"),
                new ElimpUser("Mr.Hovik"),
                new ElimpUser("NastyaVadko284"),
                //new ElimpUser("papercut6820"),
                new ElimpUser("Pozitiv4ik"),
                //new ElimpUser("prostoroma"),
                new ElimpUser("Swoop"),
                new ElimpUser("v_7946"),
                new ElimpUser("Versuzzz"),
                //new ElimpUser("Xsqten"),
                new ElimpUser("Enosha"),
                new ElimpUser("tur4ik"),
                new ElimpUser("DiMaNsKi"),
                new ElimpUser("cerepawka")
            };

            var group = new StudyGroup(users);
            group.ProblemPackList.Add(new ProblemPackInfo("A", TaskPackStorage.TasksAGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("B", TaskPackStorage.TasksBGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("C", TaskPackStorage.TasksCGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("D", TaskPackStorage.TasksDGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("E", TaskPackStorage.TasksEGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("F", TaskPackStorage.TasksFGroup, 300));

            return group;
        }
    }
}