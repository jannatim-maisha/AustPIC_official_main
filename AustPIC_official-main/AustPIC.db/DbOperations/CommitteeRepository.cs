using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AustPIC.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace AustPIC.db.DbOperations
{
    public class CommitteeRepository
    {

        public int AddCommittee(CommitteeModel model)
        {
            using (var context = new AustPICEntities())
            {
                Committee com = new Committee()
                {
                    CommitteeName = model.CommitteeName,
                    CommitteePosition = model.CommitteePosition,
                    CommitteeDetails = model.CommitteeDetails,
                    CommitteeSemester = model.CommitteeSemester,
                    CommitteeYear = model.CommitteeYear,
                    CommitteeEmail = model.CommitteeEmail,
                    CommitteeFacebook = model.CommitteeFacebook,
                    CommitteeImg = model.CommitteeImg,
                    CommitteeStatus= model.CommitteeStatus
                };

                context.Committee.Add(com);
                context.SaveChanges();

                return com.CommitteeId;
            }
        }

        public List<CommitteeModel> GetAllCommittee()
        {
            using (var context = new AustPICEntities())
            {
                var result = context.Committee.OrderBy(s => s.CommitteeStatus).Select(x => new CommitteeModel()
                {
                    CommitteeId = x.CommitteeId,
                    CommitteeName = x.CommitteeName,
                    CommitteePosition = x.CommitteePosition,
                    CommitteeDetails = x.CommitteeDetails,
                    CommitteeEmail = x.CommitteeEmail,
                    CommitteeFacebook = x.CommitteeFacebook,
                    CommitteeImg = x.CommitteeImg,
                    CommitteeYear = x.CommitteeYear,
                    CommitteeSemester = x.CommitteeSemester,
                    CommitteeStatus = x.CommitteeStatus
                }).ToList();

                return result;
            }
        }

        public CommitteeModel GetCommittee(int id)
        {
            using (var context = new AustPICEntities())
            {
                var result = context.Committee
                    .Where(x => x.CommitteeId == id)
                    .Select(x => new CommitteeModel()
                    {
                        CommitteeId = x.CommitteeId,
                        CommitteeName = x.CommitteeName,
                        CommitteePosition = x.CommitteePosition,
                        CommitteeDetails = x.CommitteeDetails,
                        CommitteeEmail = x.CommitteeEmail,
                        CommitteeFacebook = x.CommitteeFacebook,
                        CommitteeImg = x.CommitteeImg,
                        CommitteeYear = x.CommitteeYear,
                        CommitteeSemester = x.CommitteeSemester,
                        CommitteeStatus = x.CommitteeStatus
                    }).FirstOrDefault();

                return result;
            }
        }

        public bool UpdateCommittee(int id, CommitteeModel model)
        {
            using (var context = new AustPICEntities())
            {
                var committee = new Committee();

                if (committee != null)
                {
                    committee.CommitteeId = model.CommitteeId;
                    committee.CommitteeDetails = model.CommitteeDetails;
                    committee.CommitteeName = model.CommitteeName;
                    committee.CommitteePosition = model.CommitteePosition;
                    committee.CommitteeYear = model.CommitteeYear;
                    committee.CommitteeSemester = model.CommitteeSemester;
                    committee.CommitteeEmail = model.CommitteeEmail;
                    committee.CommitteeFacebook = model.CommitteeFacebook;
                    committee.CommitteeImg = model.CommitteeImg;
                    committee.CommitteeStatus = model.CommitteeStatus;
                }

                context.Entry(committee).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return true;

            }
        }
        public bool DeleteCommittee(int id)
        {
            using (var context = new AustPICEntities())
            {
                var committee = new Committee()
                {
                    CommitteeId = id
                };

                context.Entry(committee).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();

                return false;
            }
        }
    }
}
