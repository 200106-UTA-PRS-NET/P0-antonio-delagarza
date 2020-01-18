create database PizzaDB;
go;
use PizzaDB;
go;
create schema PizzaBox;
go;

------------Users-------------------
create table PizzaBox.Users(
	email varchar not null,
	password varchar not null,
	first_name varchar,
	last_name varchar,
	phone varchar(10) not null,
	--constraints
	constraint pk_users primary key(email),
	constraint phone_chk check (len(phone) = 9),
	constraint pass_len check (len(password) >=6),
	constraint phone_unique unique(phone)
);

--------Pizza table----------------
create table PizzaBox.Pizzas(
	pizzaId int identity(1, 1), 
	size varchar not null,
	crust varchar not null,
	crustFlavor varchar,
	sauce varchar not null,
	sauceAmount varchar not null,
	cheeseAmount varchar not null,
	topping1 varchar not null,
	topping2 varchar,
	topping3 varchar,
	veggie1 varchar,
	veggie2 varchar,
	veggie3 varchar,
	price money,
	--constraints
	constraint pk_pizzas primary key(pizzaId)

);

-------------Orders tables----------
create table PizzaBox.OrdersUserInfo(
	orderId int identity (1,1),
	email varchar not null,
	orderDateTime datetime not null,
	--constraints
	constraint pk_order_User_Info primary key (orderId),
	constraint fk_users foreign key(email) references PizzaBox.Users(email)
)

create table PizzaBox.OrdersPizzaInfo(
	orderId int not null,
	pizzaId int not null,
	price money not null,
	-- constraints
	constraint pk_order_Pizza_Info primary key (orderId, pizzaId),
	constraint fk_Orders_User foreign key (orderId) references PizzaBox.OrdersUserInfo(orderId),
	constraint fk_Pizza foreign key (orderId) references PizzaBox.Pizzas(pizzaId)
);

---------------Store tables----------------------------------
create table PizzaBox.StoreInfo(
	storeId int identity(1, 1),
	storeName varchar not null,
	address varchar  not null,
	city varchar not null,
	state varchar not null,
	zipCode varchar not null,
	storePrice money not null,
	--constraints
	constraint pk_store primary key(storeId)

);

create table PizzaBox.StoreOrdersInfo(
	storeId int,
	orderId int,
	--constraints
	constraint pk_store_orders primary key(storeId, orderId),
	constraint fk_store foreign key(storeId) references PizzaBox.StoreInfo(storeId),
	constraint fk_orders foreign key(orderId) references PizzaBox.OrdersUserInfo(orderId)
);

select * from PizzaBox.Users;