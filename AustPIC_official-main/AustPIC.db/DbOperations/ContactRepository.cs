//using System.Collections.Generic;
//using System.Linq;
//using AustPIC.Models;

//namespace AustPIC.db.DbOperations
//{
//    public class ContactRepository
//    {
//        public int AddContact(ContactModel model)
//        {
//            using (var context = new AustPICEntities())
//            {
//                Contact contact = new Contact()
//                {
//                    ContactEmail = model.ContactEmail,
//                    ContactName = model.ContactName,
//                    ContactSubject = model.ContactSubject,
//                    ContactMessage = model.ContactMessage,
//                    ContactTime = System.DateTime.Now
//                };

//                context.Contact.Add(contact);
//                context.SaveChanges();

//                return contact.ContactId;
//            }
//        }

//        public List<ContactModel> GetAllContact()
//        {
//            using (var context = new AustPICEntities())
//            {
//                var result = context.Contact.OrderBy(s=> s.ContactReplied).Select(x => new ContactModel()
//                {
//                    ContactId = x.ContactId,
//                    ContactEmail = x.ContactEmail,
//                    ContactSubject = x.ContactSubject,
//                    ContactMessage = x.ContactMessage,
//                    ContactName = x.ContactName,
//                    ContactReplied = x.ContactReplied,
//                    ContactTime = x.ContactTime
//                }).ToList();

//                return result;

//                //if (result.Count > 0 )
//                //{
//                //    return result;
//                //}
//                //else
//                //{
//                //    return null;
//                //}
//            }
//        }

//        public ContactModel GetContact(int id)
//        {
//            using (var context = new AustPICEntities())
//            {
//                var result = context.Contact
//                    .Where(x => x.ContactId == id)
//                    .Select(x => new ContactModel()
//                    {
//                        ContactId = x.ContactId,
//                        ContactEmail = x.ContactEmail,
//                        ContactMessage = x.ContactMessage,
//                        ContactSubject = x.ContactSubject,
//                        ContactName = x.ContactName,
//                        ContactReplied = x.ContactReplied,
//                        ContactTime = x.ContactTime
//                    }).FirstOrDefault();

//                return result;
//            }
//        }

//        public ContactModel UpdateReplyStatus(int id)
//        {
//            using (var context = new AustPICEntities())
//            {
//                var result = context.Contact.Where(b => b.ContactId == id).ToList();
//                if (result.Any())
//                {
//                    foreach (var status in result)
//                    {
//                        status.ContactReplied = 1;
//                    }

//                    context.SaveChanges();
//                }
//            }
//            return null;
//        }

//    }
//}
