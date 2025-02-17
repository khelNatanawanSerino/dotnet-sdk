﻿using System;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.Terminals.Abstractions;
using GlobalPayments.Api.Terminals.Builders;
using GlobalPayments.Api.Terminals.Ingenico;
using GlobalPayments.Api.Terminals.Messaging;

namespace GlobalPayments.Api.Terminals {
    public abstract class DeviceInterface<T> : IDeviceInterface where T : DeviceController {
        protected T _controller;
        protected IRequestIdProvider _requestIdProvider;

        public event MessageSentEventHandler OnMessageSent;
        public event BroadcastMessageEventHandler OnBroadcastMessage;
        public event PayAtTableRequestEventHandler OnPayAtTableRequest;

        internal DeviceInterface(T controller) {
            _controller = controller;
            _controller.OnMessageSent += (message) => {
                OnMessageSent?.Invoke(message);
            };

            _controller.OnBroadcastMessage += (code, message) => {
                OnBroadcastMessage?.Invoke(code, message);
            };

            _controller.OnPayAtTableRequest += (request) => {
                OnPayAtTableRequest?.Invoke(request);
            };


            _requestIdProvider = _controller.RequestIdProvider;
        }

        #region Admin Methods
        public virtual IDeviceResponse Cancel() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse CloseLane() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse DisableHostResponseBeep() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual ISignatureResponse GetSignatureFile() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IInitializeResponse Initialize() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse LineItem(string leftText, string rightText = null, string runningLeftText = null, string runningRightText = null) {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse OpenLane() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual ISignatureResponse PromptForSignature(string transactionId = null) {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse Reboot() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse Reset() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual string SendCustomMessage(DeviceMessage message) {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse SendFile(SendFileType fileType, string filePath) {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual ISAFResponse SendStoreAndForward() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse SetStoreAndForwardMode(bool enabled) {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse StartCard(PaymentMethodType paymentMethodType) {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse Duplicate() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse GetTerminalStatus() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse GetTerminalConfiguration() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IDeviceResponse TestConnection() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }
        #endregion

        #region Batching
        public virtual IBatchCloseResponse BatchClose() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }

        public virtual IEODResponse EndOfDay() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }
        #endregion

        #region Reporting Methods
        public virtual TerminalReportBuilder LocalDetailReport() {
            throw new UnsupportedTransactionException("This function is not supported by the currently configured device.");
        }
        public virtual TerminalReportBuilder GetLastReceipt(ReceiptType type = ReceiptType.TICKET) {
            return new TerminalReportBuilder(type);
        }

        public virtual TerminalReportBuilder GetReport(Ingenico.ReportType type) {
            //return new TerminalReportBuilder(TerminalReportType.LocalDetailReport).WithReportType(type);
            return new TerminalReportBuilder(type);
        }

        #endregion

        #region Transactions
        public virtual TerminalAuthBuilder AddValue(decimal? amount = null) {
            return new TerminalAuthBuilder(TransactionType.AddValue, PaymentMethodType.Gift)
                .WithAmount(amount);
        }
        public virtual TerminalAuthBuilder Authorize(decimal? amount = null) {
            return new TerminalAuthBuilder(TransactionType.Auth, PaymentMethodType.Credit)
                .WithAmount(amount);
        }
        public virtual TerminalAuthBuilder Balance() {
            return new TerminalAuthBuilder(TransactionType.Balance, PaymentMethodType.Gift);
        }
        public virtual TerminalManageBuilder Capture(decimal? amount = null) {
            return new TerminalManageBuilder(TransactionType.Capture, PaymentMethodType.Credit)
                .WithAmount(amount);
        }
        public virtual TerminalAuthBuilder Refund(decimal? amount = null) {
            return new TerminalAuthBuilder(TransactionType.Refund, PaymentMethodType.Credit)
                .WithAmount(amount);
        }
        public virtual TerminalAuthBuilder Sale(decimal? amount = null) {
            return new TerminalAuthBuilder(TransactionType.Sale, PaymentMethodType.Credit)
                .WithAmount(amount);
        }

        public virtual TerminalAuthBuilder Verify() {
            return new TerminalAuthBuilder(TransactionType.Verify, PaymentMethodType.Credit)
                .WithAmount(6.18m);
        }
        public virtual TerminalManageBuilder Void() {
            return new TerminalManageBuilder(TransactionType.Void, PaymentMethodType.Credit);
        }
        public virtual TerminalAuthBuilder Withdrawal(decimal? amount = null) {
            return new TerminalAuthBuilder(TransactionType.BenefitWithdrawal, PaymentMethodType.EBT)
                .WithAmount(amount);
        }
        #endregion

        #region IDisposable
        public void Dispose() {
            _controller.Dispose();
        }
        #endregion

        #region For clarification

        #region Transaction Management
        public virtual TerminalManageBuilder Reverse(decimal? amount = null) {
            return new TerminalManageBuilder(TransactionType.Reversal, PaymentMethodType.Credit)
                .WithAmount(amount);
        }

        #endregion

        #region Pay@Table Methods

        public virtual TerminalAuthBuilder PayAtTableResponse() {
            return new TerminalAuthBuilder(TransactionType.PayAtTable, PaymentMethodType.Other);
        }

        #endregion

        public virtual TerminalManageBuilder ReferralConfirmation() {
            return new TerminalManageBuilder(TransactionType.Referral, PaymentMethodType.Credit)
                .WithAmount(6.18m);
        }

        #endregion
    }
}
