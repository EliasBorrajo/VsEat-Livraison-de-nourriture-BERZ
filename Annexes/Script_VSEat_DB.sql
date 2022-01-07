if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLIENT') and o.name = 'FK_CLIENT_REFERENCE_LOCALITE')
alter table CLIENT
   drop constraint FK_CLIENT_REFERENCE_LOCALITE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COMMANDE') and o.name = 'FK_COMMANDE_REFERENCE_STAFF')
alter table COMMANDE
   drop constraint FK_COMMANDE_REFERENCE_STAFF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COMMANDE') and o.name = 'FK_COMMANDE_REFERENCE_CLIENT')
alter table COMMANDE
   drop constraint FK_COMMANDE_REFERENCE_CLIENT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COMMANDEPLAT') and o.name = 'FK_COMMANDE_REFERENCE_COMMANDE')
alter table COMMANDEPLAT
   drop constraint FK_COMMANDE_REFERENCE_COMMANDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COMMANDEPLAT') and o.name = 'FK_COMMANDE_REFERENCE_PLAT')
alter table COMMANDEPLAT
   drop constraint FK_COMMANDE_REFERENCE_PLAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PLAT') and o.name = 'FK_PLAT_REFERENCE_RESTAURA')
alter table PLAT
   drop constraint FK_PLAT_REFERENCE_RESTAURA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RESTAURANT') and o.name = 'FK_RESTAURA_REFERENCE_LOCALITE')
alter table RESTAURANT
   drop constraint FK_RESTAURA_REFERENCE_LOCALITE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('STAFFLOCALITE') and o.name = 'FK_STAFFLOC_REFERENCE_STAFF')
alter table STAFFLOCALITE
   drop constraint FK_STAFFLOC_REFERENCE_STAFF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('STAFFLOCALITE') and o.name = 'FK_STAFFLOC_REFERENCE_LOCALITE')
alter table STAFFLOCALITE
   drop constraint FK_STAFFLOC_REFERENCE_LOCALITE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLIENT')
            and   type = 'U')
   drop table CLIENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COMMANDE')
            and   type = 'U')
   drop table COMMANDE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COMMANDEPLAT')
            and   type = 'U')
   drop table COMMANDEPLAT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOCALITE')
            and   type = 'U')
   drop table LOCALITE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PLAT')
            and   type = 'U')
   drop table PLAT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RESTAURANT')
            and   type = 'U')
   drop table RESTAURANT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('STAFF')
            and   type = 'U')
   drop table STAFF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('STAFFLOCALITE')
            and   type = 'U')
   drop table STAFFLOCALITE
go

/*==============================================================*/
/* Table : CLIENT                                               */
/*==============================================================*/
create table CLIENT (
   CLIID                int                  not null,
   LOCID                int                  not null,
   CLINOM               varchar(250)         not null,
   CLIPRENOM            varchar(250)         not null,
   CLITELEPHONE         varchar(15)          null,
   CLIMAIL              varchar(250)         not null,
   CLIADRESSE           varchar(250)         not null,
   CLIPASSWORD          varchar(250)         not null,
   CLISTATUS            tinyint              not null,
   constraint PK_CLIENT primary key (CLIID)
)
go

/*==============================================================*/
/* Table : COMMANDE                                             */
/*==============================================================*/
create table COMMANDE (
   COMID                int                  not null,
   STAID                int                  null,
   CLIID                int                  not null,
   COMHEURE             datetime             not null,
   COMHEURELIVRAISON    datetime             not null,
   COMHEUREPAIEMENT     datetime             null,
   COMSOMME             double precision     not null,
   COMANNULE            tinyint              not null,
   constraint PK_COMMANDE primary key (COMID)
)
go

/*==============================================================*/
/* Table : COMMANDEPLAT                                         */
/*==============================================================*/
create table COMMANDEPLAT (
   COMID                int                  not null,
   PLATID               int                  not null,
   CPQUANTITE           int                  not null,
   constraint PK_COMMANDEPLAT primary key (COMID, PLATID)
)
go

/*==============================================================*/
/* Table : LOCALITE                                             */
/*==============================================================*/
create table LOCALITE (
   LOCID                int                  not null,
   LOCNOM               varchar(250)         not null,
   LOCNPA               varchar(10)          not null,
   constraint PK_LOCALITE primary key (LOCID)
)
go

/*==============================================================*/
/* Table : PLAT                                                 */
/*==============================================================*/
create table PLAT (
   PLATID               int                  not null,
   RESID                int                  not null,
   PLATNOM              varchar(50)          not null,
   PLATPRIX             double precision     not null,
   PLATDESCRIPTION      varchar(250)         null,
   PLATIMAGE            varbinary(MAX)       null,
   constraint PK_PLAT primary key (PLATID)
)
go

/*==============================================================*/
/* Table : RESTAURANT                                           */
/*==============================================================*/
create table RESTAURANT (
   RESID                int                  not null,
   LOCID                int                  not null,
   RESNOM               varchar(250)         not null,
   RESADRESSE           varchar(250)         not null,
   RESIMAGE             varbinary(MAX)       null,
   constraint PK_RESTAURANT primary key (RESID)
)
go

/*==============================================================*/
/* Table : STAFF                                                */
/*==============================================================*/
create table STAFF (
   STAID                int                  not null,
   STANOM               varchar(250)         not null,
   STAPRENOM            varchar(250)         not null,
   STATELEPHONE         varchar(15)          not null,
   STAMAIL              varchar(250)         not null,
   STAPASSWORD          varchar(250)         not null,
   STASTATUS            tinyint              not null,
   constraint PK_STAFF primary key (STAID)
)
go

/*==============================================================*/
/* Table : STAFFLOCALITE                                        */
/*==============================================================*/
create table STAFFLOCALITE (
   STAID                int                  not null,
   LOCID                int                  not null,
   constraint PK_STAFFLOCALITE primary key (STAID, LOCID)
)
go

alter table CLIENT
   add constraint FK_CLIENT_REFERENCE_LOCALITE foreign key (LOCID)
      references LOCALITE (LOCID)
go

alter table COMMANDE
   add constraint FK_COMMANDE_REFERENCE_STAFF foreign key (STAID)
      references STAFF (STAID)
go

alter table COMMANDE
   add constraint FK_COMMANDE_REFERENCE_CLIENT foreign key (CLIID)
      references CLIENT (CLIID)
go

alter table COMMANDEPLAT
   add constraint FK_COMMANDE_REFERENCE_COMMANDE foreign key (COMID)
      references COMMANDE (COMID)
go

alter table COMMANDEPLAT
   add constraint FK_COMMANDE_REFERENCE_PLAT foreign key (PLATID)
      references PLAT (PLATID)
go

alter table PLAT
   add constraint FK_PLAT_REFERENCE_RESTAURA foreign key (RESID)
      references RESTAURANT (RESID)
go

alter table RESTAURANT
   add constraint FK_RESTAURA_REFERENCE_LOCALITE foreign key (LOCID)
      references LOCALITE (LOCID)
go

alter table STAFFLOCALITE
   add constraint FK_STAFFLOC_REFERENCE_STAFF foreign key (STAID)
      references STAFF (STAID)
go

alter table STAFFLOCALITE
   add constraint FK_STAFFLOC_REFERENCE_LOCALITE foreign key (LOCID)
      references LOCALITE (LOCID)
go
