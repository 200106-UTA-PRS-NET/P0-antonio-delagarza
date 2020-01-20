create database PizzaDB;
go;
use PizzaDB;
go;
create schema PizzaBox;
go;

------------Users-------------------
create table PizzaBox.Users(
	email varchar(100) not null,
	password varchar(100) not null,
	first_name varchar(100),
	last_name varchar(100),
	phone varchar(100) not null,
	--constraints
	constraint pk_users primary key(email),
	constraint phone_chk check (len(phone) = 9),
	constraint pass_len check (len(password) >=6),
	constraint phone_unique unique(phone)
);

--------Pizza table----------------
create table PizzaBox.Pizzas(
	pizzaId int identity(1, 1), 
	size varchar(100) not null,
	crust varchar(100) not null,
	crustFlavor varchar(100),
	sauce varchar(100) not null,
	sauceAmount varchar(100) not null,
	cheeseAmount varchar(100) not null,
	topping1 varchar(100) not null,
	topping2 varchar(100),
	topping3 varchar(100),
	price money,
	--constraints
	constraint pk_pizzas primary key(pizzaId)

);
alter table PizzaBox.Pizzas drop column veggie1, veggie2, veggie3;
-------------Orders tables----------
create table PizzaBox.OrdersUserInfo(
	orderId int identity (1,1),
	email varchar(100) not null,
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
	storeName varchar(100) not null,
	address varchar(100)  not null,
	city varchar(100) not null,
	state varchar(100) not null,
	zipCode varchar(100) not null,
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

-----------------Present Pizzas Tables----------------
create table PizzaBox.PresetPizzas(
	presetPizzaId int identity(1, 1), 
	size varchar(100) not null,
	crust varchar(100) not null,
	crustFlavor varchar(100),
	sauce varchar(100) not null,
	sauceAmount varchar(100) not null,
	cheeseAmount varchar(100) not null,
	topping1 varchar(100) not null,
	topping2 varchar(100),
	topping3 varchar(100),
	price money,
	--constraints
	constraint pk_preset_pizzas primary key(presetPizzaId),
	constraint unique_pizzas unique(size, crust, crustFlavor, sauce, sauceAmount, cheeseAmount, topping1, topping2, topping3)
);

create table PizzaBox.StorePresetPizzas (
	storeId int, 
	presetPizzaId int,
	--constraints
	constraint pk_store_preset primary key(storeId, presetPizzaId),
	constraint fk_store_info foreign key(storeId) references PizzaBox.StoreInfo(storeId),
	constraint fk_preset_pizza foreign key(presetPizzaId) references PizzaBox.PresetPizzas(presetPizzaId)

);

alter table PizzaBox.OrdersPizzaInfo drop column price;