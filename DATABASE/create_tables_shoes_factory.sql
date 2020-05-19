use shoes_factory;

CREATE TABLE drivers (
    id INT AUTO_INCREMENT,
    first_name VARCHAR(30) NOT NULL,
    last_name VARCHAR(30) NOT NULL,
    age TINYINT UNSIGNED NOT NULL,
    salary INT UNSIGNED NOT NULL,
    experience_in_years TINYINT UNSIGNED NOT NULL,
    CONSTRAINT employee_pk PRIMARY KEY (id)
);

CREATE TABLE city (
    id INT AUTO_INCREMENT,
    c_name VARCHAR(50) NOT NULL,
    CONSTRAINT city_pk PRIMARY KEY (id)
);

CREATE TABLE skin_shops (
    id INT AUTO_INCREMENT,
    city_id INT NOT NULL,
    s_name VARCHAR(50) NOT NULL,
    address VARCHAR(300) NOT NULL,
    email VARCHAR(50),
    phone BIGINT(12),
    distance INT NOT NULL,
    CONSTRAINT skin_shop_pk PRIMARY KEY (id),
    CONSTRAINT skin_shop_city_id_fk FOREIGN KEY (city_id)
        REFERENCES city (id)
);

CREATE TABLE skin_orders (
    id INT AUTO_INCREMENT,
    skin_shop_id INT NOT NULL,
    driver_id INT NOT NULL,
    skin_sq_foots_to_deliver INT NOT NULL,
    max_days_to_deliver INT NOT NULL,
    CONSTRAINT skin_orders_pk PRIMARY KEY (id),
    CONSTRAINT skin_orders_skin_shop_id_fk FOREIGN KEY (skin_shop_id)
        REFERENCES skin_shops (id),
    CONSTRAINT skin_orders_driver_id_fk FOREIGN KEY (driver_id)
        REFERENCES drivers (id)
);

CREATE TABLE completed_deliveries (
    id INT AUTO_INCREMENT,
    skin_order_id INT NOT NULL,
    days_on_the_road INT NOT NULL,
    CONSTRAINT completed_deliveries_pk PRIMARY KEY (id),
    CONSTRAINT completed_deliveries_skin_order_id_fk FOREIGN KEY (skin_order_id)
        REFERENCES skin_orders (id)
);

CREATE TABLE category (
    id INT AUTO_INCREMENT,
    c_name VARCHAR(50) NOT NULL,
    c_description VARCHAR(4000),
    CONSTRAINT category_pk PRIMARY KEY (id)
);

CREATE TABLE shoes (
    id INT AUTO_INCREMENT,
    category_id INT NOT NULL,
    s_name VARCHAR(50) NOT NULL,
    s_description VARCHAR(4000),
    size INT NOT NULL,
    price DECIMAL NOT NULL,
    production_time INT NOT NULL,
    required_skin_sq_foots_to_product INT NOT NULL,
    CONSTRAINT shoes_pk PRIMARY KEY (id),
    CONSTRAINT shoes_category_id_fk FOREIGN KEY (category_id)
        REFERENCES category (id)
);

CREATE TABLE skin_stock (
    id INT AUTO_INCREMENT,
    free_space INT NOT NULL,
    max_space INT NOT NULL,
    CONSTRAINT skin_stock_pk PRIMARY KEY (id)
);