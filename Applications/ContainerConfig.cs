using System;
using System.Collections.Generic;
using System.Text;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Autofac;
using Commands;
using Core;
using DepositModule.CreateDeposit;
using DepositModule.Write;
using Events;
using Models;
using ReadDB;

namespace Applications
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<TestApplication>().As<ITestApplication>();

            builder.RegisterType<SqLiteCtxFactoryMethod>().As<IDbContextFactoryMethod<ModelDbContext>>()
                .SingleInstance();
            builder.RegisterType<DefaultEventStore>().As<IEventStore>().SingleInstance();

            builder.RegisterType<Cache<IEvent>>().As<ICache<IEvent>>();
            builder.RegisterType<Cache<ICommand>>().As<ICache<ICommand>>();

            builder.RegisterType<Bus<ICommand>>().As<IBus<ICommand>>().SingleInstance();
            builder.RegisterType<Bus<IEvent>>().As<IBus<IEvent>>().SingleInstance();

            builder.RegisterType<EventRepository>().As<IEventRepository>().SingleInstance();
            builder.RegisterType<AccountModelRepository>().As<IModelRepository<AccountModel>>().SingleInstance();
            builder.RegisterType<DepositModelRepository>().As<IModelRepository<DepositModel>>().SingleInstance();
            builder.RegisterType<AtmModelRepository>().As<IModelRepository<AtmModel>>().SingleInstance();

            builder.RegisterType<Service<AccountModel>>().As<IService<AccountModel>>().SingleInstance();
            builder.RegisterType<Service<DepositModel>>().As<IService<DepositModel>>().SingleInstance();
            builder.RegisterType<Service<AtmModel>>().As<IService<AtmModel>>().SingleInstance();

            builder.RegisterType<AggregateService<AccountAggregate>>().As<IAggregateService<AccountAggregate>>()
                .SingleInstance();
            builder.RegisterType<AggregateService<DepositAggregate>>().As<IAggregateService<DepositAggregate>>()
                .SingleInstance();

            builder.RegisterType<AccountAggregateFactoryMethod>().As<IAggregateFactoryMethod<AccountAggregate>>();
            builder.RegisterType<DepositAggregateFactoryMethod>().As<IAggregateFactoryMethod<DepositAggregate>>();

            builder.RegisterType<ObservableEventPublisher>().As<IEventPublisher>().AsSelf().SingleInstance();

            builder.RegisterType<BusObserverAdapter<IEvent>>();

            builder.RegisterType<CreateAccountEventHandler>().As<IEventHandler<CreateAccountEvent>>();
            builder.RegisterType<CreateDepositEventHandler>().As<IEventHandler<CreateDepositEvent>>();
            builder.RegisterType<AddDepositToAccountEventHandler>().As<IEventHandler<AddDepositToAccountEvent>>();

            builder.RegisterType<CreateAccountCommandHandler>().As<ICommandHandler<CreateAccountCommand>>();
            builder.RegisterType<CreateDepositCommandHandler>().As<ICommandHandler<CreateDepositCommand>>();

            builder.RegisterType<HandlerFactoryMethod<ICommand>>().As<IHandlerFactoryMethod<ICommand>>()
                .SingleInstance();
            builder.RegisterType<HandlerFactoryMethod<IEvent>>().As<IHandlerFactoryMethod<IEvent>>().SingleInstance();

            return builder.Build();
        }
    }
}