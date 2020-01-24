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
	first_name varchar(100) not null,
	last_name varchar(100) not null,
	--constraints
	constraint pk_users primary key(email),
	constraint pass_len check (len(password) >=6),

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
	topping1 varchar(100),
	topping2 varchar(100),
	topping3 varchar(100),
	price money not null,
	--constraints
	constraint pk_pizzas primary key(pizzaId)

);

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
	-- constraints
	constraint pk_order_Pizza_Info primary key (orderId, pizzaId),
	constraint fk_Orders_User foreign key (orderId) references PizzaBox.OrdersUserInfo(orderId),
	constraint fk_Pizza foreign key (pizzaId) references PizzaBox.Pizzas(pizzaId)
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
	constraint pk_store primary key(storeId),
	constraint unique_stores unique(storeName, address, city, state, zipCode)


);



create table PizzaBox.StoreOrdersInfo(
	storeId int not null,
	orderId int not null,
	--constraints
	constraint pk_store_orders primary key(storeId, orderId),
	constraint fk_store foreign key(storeId) references PizzaBox.StoreInfo(storeId),
	constraint fk_orders foreign key(orderId) references PizzaBox.OrdersUserInfo(orderId)
);

-----------------Preset Pizzas Tables----------------
create table PizzaBox.PresetPizzas(
	pizzaName varchar(100), 
	size varchar(100) not null,
	crust varchar(100) not null,
	crustFlavor varchar(100),
	sauce varchar(100) not null,
	sauceAmount varchar(100) not null,
	cheeseAmount varchar(100) not null,
	topping1 varchar(100),
	topping2 varchar(100),
	topping3 varchar(100),
	price money not null,
	--constraints
	constraint pk_preset_pizzas primary key(pizzaName),
	constraint unique_pizzas unique(size, crust, crustFlavor, sauce, sauceAmount, cheeseAmount, topping1, topping2, topping3)
);

create table PizzaBox.StorePresetPizzas (
	storeId int not null, 
	pizzaName varchar(100) not null,
	--constraints
	constraint pk_store_preset primary key(storeId, pizzaName),
	constraint fk_store_info foreign key(storeId) references PizzaBox.StoreInfo(storeId),
	constraint fk_preset_pizza foreign key(pizzaName) references PizzaBox.PresetPizzas(pizzaName)

);

