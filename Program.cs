using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Configuration;

namespace ADQuerier
{
    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
    internal class Program
    {
        private const string LthtDomainName = "trust.leedsth.nhs.uk";
        private const string MyhtDomainName = "10.146.240.200:389";
        private static string LthtAdPassword;
        private static string MyAdPassword;

        private static void Main()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            LthtAdPassword = config["LthtAdPassword"];
            MyAdPassword = config["MyAdPassword"];
            
            
            SearchLthtDirectory("thoresbn");
            //SearchMythDirectory("Amina.Rawat1");
            //SearchBySid("S-1-5-21-3363467310-3950875991-3575587589-23641");

            Console.Read();
        }

        //private static void SearchBySid(string sid)
        //{
        //    using var ctx = new PrincipalContext(ContextType.Domain, DomainName);
        //    var user = UserPrincipal.FindByIdentity(ctx, sid);

        //    if (user == null)
        //    {
        //        Console.WriteLine($"SID [{sid}] not found in [{DomainName}]");
        //        return;
        //    }

        //    Console.WriteLine($"{user.Sid} found user {user.SamAccountName} : [{DomainName}]");
        //    Console.WriteLine($"{user.DisplayName}");
        //    Console.WriteLine($"{user.UserPrincipalName}");
        //}

        private static void SearchLthtDirectory(string accountName)
        {
            using var ctx = new PrincipalContext(ContextType.Domain, LthtDomainName, "thoresbn", LthtAdPassword);
            
            using var searcher = new PrincipalSearcher(new UserPrincipal(ctx) { SamAccountName = accountName });
            var user = searcher.FindOne();
            if (user == null)
            {
                Console.WriteLine($"Account [{accountName}] not found in [{LthtDomainName}]");
                return;
            }

            Console.WriteLine($"{user.SamAccountName} has SID = {user.Sid} : [{LthtDomainName}]");
            Console.WriteLine($"{user.DisplayName}");
            Console.WriteLine($"{user.UserPrincipalName}");
            
            // Console.WriteLine();
            // user.GetGroups().ToList().ForEach(p => Console.WriteLine(p.Name));
        }
        
        private static void SearchMythDirectory(string accountName)
        {
            using var ctx = new PrincipalContext(ContextType.Domain, MyhtDomainName, "SVC_PPMIdentityServer_UAT@midyorks.nhs.uk", MyAdPassword);
            using var searcher = new PrincipalSearcher(new UserPrincipal(ctx) { SamAccountName = accountName });
            var user = searcher.FindOne();
            if (user == null)
            {
                Console.WriteLine($"Account [{accountName}] not found in [midyorks.nhs.uk]");
                return;
            }

            Console.WriteLine($"{user.SamAccountName} has SID = {user.Sid} : [midyorks.nhs.uk]");
            Console.WriteLine($"{user.DisplayName}");
            Console.WriteLine($"{user.UserPrincipalName}");

            Console.WriteLine();
            // user.GetGroups().ToList().ForEach(p => Console.WriteLine(p.Name));
        }        
    }
}