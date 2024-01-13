create table tblGroup
(
	GroupID int identity(1,1) primary key,
	GroupName nvarchar(50) not null,
	CreatedDate datetime,
	CreatedByUserID int not null,
	GroupDescription nvarchar(100),
	isActive bit

	foreign key (CreatedByUserID) references tblUser(UserID)
)

create table tblMessage
(
	MessageID int identity(1,1) primary key,
	SenderUserID int not null,
	ChatID int not null,
	MessageText nvarchar(200) not null,
	Timestampt datetime not null

	foreign key (SenderUserID) references tblUser(UserID),
	foreign key (ChatID) references tblPrivateChat(ChatID)
)

create table tblPrivateChat
(
	ChatID int identity(1,1) primary key,
	User1ID int not null,
	User2ID int not null,
	LastMessageID int not null

	foreign key (User1ID) references tblUser(UserID),
	foreign key (User2ID) references tblUser(UserID),
	
)

create table tblUser
(
	UserID int identity(1,1) primary key,
	Username nvarchar(20) not null,
	FirstName nvarchar(20),
	LastName nvarchar(20),
	Email nvarchar(100) not null,
	HashedPassword nvarchar(100) not null,
	ProfilePictureURL nvarchar(max),
	LastLogin datetime,
	CreatedAt datetime,
	Status nvarchar(20)
)

create table tblUserGroup
(
	UserID int not null,
	GroupID int not null,
	JoinedDate datetime,
	UserRole nvarchar(20)

	foreign key (UserID) references tblUser(UserID)
)


alter table tblPrivateChat
add LastMessageID int constraint FK_MessageID 
	foreign key (LastMessageID) references tblMessage(MessageID)