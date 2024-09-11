﻿using Exwhyzee.Messaging.Web.Dtos;
using Exwhyzee.Messaging.Web.Models;
using Exwhyzee.Messaging.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exwhyzee.Messaging.Web.Data.IServices
{
    public interface IClientService
    {
        Task<Client> GetClientDetails(int clientId);

        Task<Client> GetClientDetailsByUserId(string userId);

        Task UpdateClient(Client item);

        Task AddUnit(int id, Transaction item);

        Task<decimal> GetAmount(decimal units);

        Task<List<Transaction>> GetUserTransactions(int id);

        ///
        ///USERS END
        ///

        Task<SmsResponse> SendSms(string senderId, string message, string recipients);

        Task<SmsResponse> SendSmsById(int messageHistoryId, decimal units);

        Task<List<Message>> GetClientMessageHistory(string userId);

        Task<Message> GetMessage(int? id);

        Task<List<Message>> GetAllMessageHistory();

        Task<List<Message>> GetClientDraftMessages(string userId);

        Task LoadVoucher(Voucher item);

        Task<Voucher> CheckVoucher(string code, string batchNumber);

        Task AddMessageToHistory(Message item);

        Task UpdateMessageStatus(Message item);

        Task<SendMessageResponseDto> ComposeSms(ComposeSmsDto model, string userId);

        Task<GeneralResponse> SubmitSenderId(string senderId, string senderMessage);
        Task<GeneralResponse> VerifySenderId(string senderId);

        Task<string> AddSender(string userId, string senderId, string message);
        Task<string> VerifySender(string senderId);
        Task<List<XyzSenderID>> GetAllSenderId();
        Task<List<XyzSenderID>> GetAllSenderIdById(string userId);

    }
}