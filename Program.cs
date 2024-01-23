using System;
using System.DirectoryServices.AccountManagement;
// using System.Linq;

namespace ADQuerier
{
    internal class Program
    {
        private const string LthtDomainName = "trust.leedsth.nhs.uk";
        private const string MyhtDomainName = "10.146.240.200:389";

        private static void Main()
        {
            SearchLthtDirectory("thoresbn");
            //SearchMythDirectory("Sarah.Zurub1");
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
            using var ctx = new PrincipalContext(ContextType.Domain, LthtDomainName, "thoresbn", "Chatsworth36");


            // var domain = Domain.GetDomain(new DirectoryContext(DirectoryContextType.Domain, context.Name));
            // var domainPolicy = domain.GetDomainPolicy();
            // return domainPolicy.MaximumPasswordAge;  // This should return a TimeSpan

            
            
            
            
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

            Console.WriteLine();
            // user.GetGroups().ToList().ForEach(p => Console.WriteLine(p.Name));
        }
        
        private static void SearchMythDirectory(string accountName)
        {
            using var ctx = new PrincipalContext(ContextType.Domain, MyhtDomainName, "SVC_PPMIdentityServer_UAT@midyorks.nhs.uk", "ElSNN02t6ad");
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