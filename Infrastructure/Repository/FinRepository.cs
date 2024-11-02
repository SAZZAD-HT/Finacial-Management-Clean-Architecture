using Core.DTos;
using Core.Interfaces;
using Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class FinRepository : IFinRepository
    {
        private readonly FinManagementEntities _context;

        public FinRepository(FinManagementEntities context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserDto>> GetUser(int id)
        {
            var data = await
                (from user in _context.Users
                 join account in _context.Accounts on user.AccountId equals account.AccountId
                 where (id==0 || user.UserID == id)
                 select new UserDto
                 {
                     UserID = user.UserID,
                     UserName = user.UserName,
                     Email = user.Email,
                     Password = user.Password,
                     IsActive = user.IsActive,
                     AccountType = account.AccountType,
                     Balance = (decimal)account.Balance,
                     Description = account.Description

                 }).ToListAsync();

            return data;

        }
        public async Task<UserDto> GetUserById(int id)
        {
            var data = await
                         (from user in _context.Users
                          where user.UserID == id
                          select new UserDto
                          {
                              UserID = user.UserID,
                              UserName = user.UserName,
                              Email = user.Email,
                              Password = user.Password,
                              IsActive = user.IsActive,

                          }).FirstAsync();
            return data;
        }
        public async Task<string> AddUser(UserDto user)
        {
            string res = "";
            try
            {
                if (user.UserID == 0)
                {
                    var ff = _context.Accounts.Add(new Account
                    {
                        AccountName = user.UserName,
                        AccountType = user.AccountType,
                        Balance = user.Balance,
                        Description = user.Description,

                    });
                    _context.SaveChanges();
                    _context.Users.Add(new User
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.Password,
                        IsActive = user.IsActive,
                        AccountId = ff.AccountId,
                    });

                    res = res + " User added successfully";
                }
                else
                {
                    var account = await _context.Accounts.FindAsync(user.UserID);
                    if (account != null)
                    {
                        account.AccountName = user.UserName;
                        account.AccountType = user.AccountType;
                        account.Balance = user.Balance;
                        account.Description = user.Description;
                        _context.Accounts.AddOrUpdate(account);
                    }
                    else
                    {
                        res = res + " Account not found";
                    }
                    var data = await _context.Users.FindAsync(user.UserID);
                    if (data != null)
                    {
                        data.UserName = user.UserName;
                        data.Email = user.Email;
                        data.Password = user.Password;
                        data.IsActive = user.IsActive == null ? true : user.IsActive;
                        _context.Users.AddOrUpdate(data);
                        res = res + "User updated successfully";
                    }
                    else
                    {
                        res = res + " User not found";
                    }
                }

                await _context.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                return res = ex.Message;
            }
        }

        public async Task<string> JournalAddUpdateDeleteDto(JournalAddDto dd)
        {
            string res;
            try
            {
                if (dd.EntryId == 0)
                {
                    JournalEntry journalEntry = new JournalEntry
                    {
                        Date = dd.Date,
                        Description = dd.Description,
                        TotalDebit = dd.JournalDetailAddDtos.Sum(x => x.Debit),
                        TotalCredit = dd.JournalDetailAddDtos.Sum(x => x.Credit),
                        IsActive = true,
                        CreatedTime = DateTime.Now,
                        ActionBy = dd.ActionBy
                    };
                    _context.JournalEntries.Add(journalEntry);
                    await _context.SaveChangesAsync();

                    foreach (var item in dd.JournalDetailAddDtos)
                    {
                        JournalEntryLine journalEntryLine = new JournalEntryLine
                        {

                            AccountId = item.AccountId,
                            DebitAmount = item.Debit,
                            CreditAmount = item.Credit,
                            Description = item.Description,
                            JEId = journalEntry.EntryId
                        };

                        _context.JournalEntryLines.Add(journalEntryLine);
                    }
                    await _context.SaveChangesAsync();
                    res = "Journal entry added successfully";
                }
                else
                {
                    var data = await _context.JournalEntries.FindAsync(dd.EntryId);
                    if (data != null)
                    {
                        data.Date = dd.Date;
                        data.Description = dd.Description;
                        data.TotalDebit = dd.JournalDetailAddDtos.Sum(x => x.Debit);
                        data.TotalCredit = dd.JournalDetailAddDtos.Sum(x => x.Credit);
                        data.IsActive = dd.IsActive;
                        data.CreatedTime = dd.CreatedTime;
                        data.ActionBy = dd.ActionBy;
                        _context.JournalEntries.AddOrUpdate(data);
                        res = "Journal entry updated successfully";
                        foreach (var item in dd.JournalDetailAddDtos)
                        {
                            if (item.EntryDetailId == 0)
                            {
                                JournalEntryLine journalEntryLine = new JournalEntryLine
                                {
                                    AccountId = item.AccountId,
                                    DebitAmount = item.Debit,
                                    CreditAmount = item.Credit,
                                    Description = item.Description,
                                    JEId = data.EntryId
                                };
                                _context.JournalEntryLines.Add(journalEntryLine);
                            }
                            else
                            {
                                var journalEntryLine = await _context.JournalEntryLines.FindAsync(item.EntryDetailId);
                                if (journalEntryLine != null)
                                {
                                    journalEntryLine.AccountId = item.AccountId;
                                    journalEntryLine.DebitAmount = item.Debit;
                                    journalEntryLine.CreditAmount = item.Credit;
                                    journalEntryLine.Description = item.Description;
                                    journalEntryLine.JEId = data.EntryId;
                                    _context.JournalEntryLines.AddOrUpdate(journalEntryLine);
                                }
                            }
                        }
                    }
                    else
                    {
                        res = "Journal entry not found";
                    }
                }

                await _context.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                return res = ex.Message;
            }
        }
        public async Task<IEnumerable<JournalAddDto>> GetallJournalInfo()
        {
            try
            {
                var data = await (from journal in _context.JournalEntries
                                  select new JournalAddDto
                                  {
                                      EntryId = journal.EntryId,
                                      Date = (DateTime)journal.Date,
                                      Description = journal.Description,
                                      TotalDebit = (decimal)journal.TotalDebit,
                                      TotalCredit = (decimal)journal.TotalCredit,
                                      IsActive = journal.IsActive,
                                      CreatedTime = (DateTime)journal.CreatedTime,
                                      ActionBy = journal.ActionBy,
                                      JournalDetailAddDtos = (from journalDetail in _context.JournalEntryLines
                                                              where journalDetail.JEId == journal.EntryId
                                                              select new JournalDetailAddDto
                                                              {
                                                                  EntryDetailId = journalDetail.JLineId,
                                                                  EntryId = journalDetail.JEId,
                                                                  AccountId = journalDetail.AccountId,
                                                                  Description = journalDetail.Description,
                                                                  Debit = journalDetail.DebitAmount,
                                                                  Credit = (decimal)journalDetail.CreditAmount,
                                                              }).ToList()
                                  }).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "GetallJournalInfo";
                throw;
            }
        }
    }
}
