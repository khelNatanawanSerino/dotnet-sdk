﻿using System;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.Terminals.Abstractions;
using GlobalPayments.Api.Terminals.Builders;
using GlobalPayments.Api.Terminals.Ingenico;
using GlobalPayments.Api.Terminals.Messaging;

namespace GlobalPayments.Api.Terminals {
    public interface IDeviceInterface : IDisposable {
        event MessageSentEventHandler OnMessageSent;
        event BroadcastMessageEventHandler OnBroadcastMessage;
        event PayAtTableRequestEventHandler OnPayAtTableRequest;

        #region Admin Calls

        /// <summary>
        /// A method to Cancel a live transaction.
        /// </summary>
        /// <param name="amount">Amount to be passed for cancel request.</param>
        /// <returns>TerminalManageBuilder</returns>
        IDeviceResponse Cancel();
        IDeviceResponse CloseLane();
        IDeviceResponse DisableHostResponseBeep();
        ISignatureResponse GetSignatureFile();
        IInitializeResponse Initialize();
        IDeviceResponse LineItem(string leftText, string rightText = null, string runningLeftText = null, string runningRightText = null);
        IDeviceResponse OpenLane();
        ISignatureResponse PromptForSignature(string transactionId = null);
        IDeviceResponse Reboot();
        IDeviceResponse Reset();
        string SendCustomMessage(DeviceMessage message);
        IDeviceResponse SendFile(SendFileType fileType, string filePath);
        ISAFResponse SendStoreAndForward();
        IDeviceResponse SetStoreAndForwardMode(bool enabled);
        IDeviceResponse StartCard(PaymentMethodType paymentMethodType);

        /// <summary>
        /// The terminal immediately initiates a duplicate of the last completed transaction
        /// </summary>
        /// <returns>IDeviceResponse</returns>
        IDeviceResponse Duplicate();

        /// <summary>
        /// Command used to gain feedback as to the status of the terminal.
        /// </summary>
        /// <returns></returns>
        IDeviceResponse GetTerminalStatus();

        /// <summary>
        /// Command used to request for CALL TMS in the terminal. 
        /// </summary>
        /// <returns>IDeviceResponse</returns>
        IDeviceResponse GetTerminalConfiguration();

        IDeviceResponse TestConnection();
        #endregion

        #region reporting
        TerminalReportBuilder LocalDetailReport();
        /// <summary>
        /// Used to request the XML data for the last completed report that is stored in the terminal’s memory
        /// </summary>
        /// <param name="type">Receipt Type</param>
        /// <returns></returns>
        TerminalReportBuilder GetLastReceipt(ReceiptType type = ReceiptType.TICKET);

        /// <summary>
        /// Instruct the terminal to initiate report and stores it in terminal's memory. 
        /// GetLastReceipt can be used to extract XML data after. 
        /// </summary>
        /// <param name="type">Report Type</param>
        /// <returns></returns>
        TerminalReportBuilder GetReport(Ingenico.ReportType type);
        #endregion

        #region Batch Calls
        IBatchCloseResponse BatchClose();
        IEODResponse EndOfDay();
        #endregion

        #region Credit Calls
        //TerminalAuthBuilder CreditAuth(decimal? amount = null);
        //TerminalManageBuilder CreditCapture(decimal? amount = null);
        //TerminalAuthBuilder CreditRefund(decimal? amount = null);
        //TerminalAuthBuilder CreditSale(decimal? amount = null);
        //TerminalAuthBuilder CreditVerify();
        //TerminalManageBuilder CreditVoid();
        #endregion

        #region Debit Calls
        //TerminalAuthBuilder DebitSale(decimal? amount = null);
        //TerminalAuthBuilder DebitRefund(decimal? amount = null);
        #endregion

        #region Gift Calls
        //TerminalAuthBuilder GiftSale(decimal? amount = null);
        //TerminalAuthBuilder GiftAddValue(decimal? amount = null);
        //TerminalManageBuilder GiftVoid();
        //TerminalAuthBuilder GiftBalance();
        #endregion

        #region EBT Calls
        //TerminalAuthBuilder EbtBalance();
        //TerminalAuthBuilder EbtPurchase(decimal? amount = null);
        //TerminalAuthBuilder EbtRefund(decimal? amount = null);
        //TerminalAuthBuilder EbtWithdrawl(decimal? amount = null);
        #endregion

        #region Generic Calls
        TerminalAuthBuilder AddValue(decimal? amount = null);

        /// <summary>
        /// Instructs the terminal to transact a pre-authorization transaction.
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <returns></returns>
        TerminalAuthBuilder Authorize(decimal? amount = null);
        TerminalAuthBuilder Balance();
        TerminalManageBuilder Capture(decimal? amount = null);

        /// <summary>
        /// Instruct the terminal to refund the last completed transaction.
        /// </summary>
        /// <param name="amount">Refund Amount</param>
        /// <returns></returns>
        TerminalAuthBuilder Refund(decimal? amount = null);

        /// <summary>
        /// Instruct the terminal to process sale transaction.
        /// </summary>
        /// <param name="amount">Sale Amount</param>
        /// <returns></returns>
        TerminalAuthBuilder Sale(decimal? amount = null);

        /// <summary>
        /// Verify the account of the card holder.
        /// </summary>
        /// <returns></returns>
        TerminalAuthBuilder Verify();
        TerminalManageBuilder Void();
        TerminalAuthBuilder Withdrawal(decimal? amount = null);
        #endregion

        #region Terminal Management

        /// <summary>
        /// The terminal immediately performs a reversal of the last completed transaction if no Transaction Id is set.
        /// </summary>
        /// <param name="amount">Amount to be passed for cancel request.</param>
        /// <returns>TerminalManageBuilder</returns>
        TerminalManageBuilder Reverse(decimal? amount = null);

        #endregion

        #region Pay@Table Feature

        /// <summary>
        /// Response to terminal after Pay@Table request has been made.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        TerminalAuthBuilder PayAtTableResponse();

        #endregion

        TerminalManageBuilder ReferralConfirmation();
    }
}
