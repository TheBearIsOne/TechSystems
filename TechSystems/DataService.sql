CREATE TABLE application_locks (
	lock_id varchar(100) NOT NULL,
	application_id int8 NOT NULL,
	locked_by int4 NOT NULL,
	locked_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	expires_at timestamp NOT NULL,
	CONSTRAINT application_locks_pkey PRIMARY KEY (lock_id)
);

CREATE TABLE clients (
	client_id bigserial NOT NULL,
	external_id varchar(50) NOT NULL,
	first_name varchar(100) NOT NULL,
	last_name varchar(100) NOT NULL,
	middle_name varchar(100) NULL,
	birth_date date NOT NULL,
	passport_series varchar(4) NULL,
	passport_number varchar(6) NULL,
	phone_number varchar(20) NOT NULL,
	email varchar(255) NULL,
	registration_address text NULL,
	residential_address text NULL,
	income numeric(15, 2) NULL,
	employment_status varchar(50) NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	updated_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	is_active bool DEFAULT true NULL,
	score int4 DEFAULT 0 NULL,
	CONSTRAINT clients_external_id_key UNIQUE (external_id),
	CONSTRAINT clients_pkey PRIMARY KEY (client_id)
);

CREATE TABLE signals (
	signal_id bigserial NOT NULL,
	signal_type varchar(100) NOT NULL,
	entity_type varchar(50) NOT NULL,
	entity_id int8 NOT NULL,
	signal_data jsonb NOT NULL,
	priority int4 DEFAULT 0 NULL,
	status varchar(50) DEFAULT 'PENDING'::character varying NULL,
	processed_at timestamp NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	expires_at timestamp NULL,
	CONSTRAINT signals_pkey PRIMARY KEY (signal_id)
);

CREATE TABLE loan_applications (
	application_id bigserial NOT NULL,
	client_id int8 NOT NULL,
	product_id int4 NOT NULL,
	requested_amount numeric(15, 2) NOT NULL,
	requested_term int4 NOT NULL,
	approved_amount numeric(15, 2) NULL,
	approved_term int4 NULL,
	interest_rate numeric(5, 2) NULL,
	status varchar(50) DEFAULT 'NEW'::character varying NOT NULL,
	rejection_reason text NULL,
	officer_id int4 NULL,
	scoring_result jsonb NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	updated_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	submitted_at timestamp NULL,
	reviewed_at timestamp NULL,
	expired_at timestamp NULL,
	CONSTRAINT loan_applications_pkey PRIMARY KEY (application_id),
	CONSTRAINT loan_applications_client_id_fkey FOREIGN KEY (client_id) REFERENCES clients(client_id)
);

CREATE TABLE loans (
	loan_id bigserial NOT NULL,
	application_id int8 NULL,
	client_id int8 NOT NULL,
	contract_number varchar(50) NOT NULL,
	principal_amount numeric(15, 2) NOT NULL,
	interest_rate numeric(5, 2) NOT NULL,
	term_months int4 NOT NULL,
	start_date date NOT NULL,
	end_date date NOT NULL,
	next_payment_date date NULL,
	current_principal numeric(15, 2) NOT NULL,
	total_paid_principal numeric(15, 2) DEFAULT 0 NULL,
	total_paid_interest numeric(15, 2) DEFAULT 0 NULL,
	status varchar(50) DEFAULT 'ACTIVE'::character varying NOT NULL,
	overdue_days int4 DEFAULT 0 NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	updated_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT loans_application_id_key UNIQUE (application_id),
	CONSTRAINT loans_contract_number_key UNIQUE (contract_number),
	CONSTRAINT loans_pkey PRIMARY KEY (loan_id),
	CONSTRAINT loans_application_id_fkey FOREIGN KEY (application_id) REFERENCES loan_applications(application_id),
	CONSTRAINT loans_client_id_fkey FOREIGN KEY (client_id) REFERENCES clients(client_id)
);

CREATE TABLE payment_schedule (
	schedule_id bigserial NOT NULL,
	loan_id int8 NOT NULL,
	payment_number int4 NOT NULL,
	due_date date NOT NULL,
	principal_amount numeric(15, 2) NOT NULL,
	interest_amount numeric(15, 2) NOT NULL,
	total_amount numeric(15, 2) NOT NULL,
	is_paid bool DEFAULT false NULL,
	paid_date date NULL,
	CONSTRAINT payment_schedule_loan_id_payment_number_key UNIQUE (loan_id, payment_number),
	CONSTRAINT payment_schedule_pkey PRIMARY KEY (schedule_id),
	CONSTRAINT payment_schedule_loan_id_fkey FOREIGN KEY (loan_id) REFERENCES loans(loan_id)
);


CREATE TABLE payments (
	payment_id bigserial NOT NULL,
	loan_id int8 NOT NULL,
	schedule_payment_id int8 NULL,
	payment_date date NOT NULL,
	due_date date NOT NULL,
	principal_amount numeric(15, 2) NOT NULL,
	interest_amount numeric(15, 2) NOT NULL,
	total_amount numeric(15, 2) GENERATED ALWAYS AS ((principal_amount + interest_amount)) STORED NULL,
	paid_principal numeric(15, 2) NULL,
	paid_interest numeric(15, 2) NULL,
	paid_total numeric(15, 2) GENERATED ALWAYS AS ((COALESCE(paid_principal, 0::numeric) + COALESCE(paid_interest, 0::numeric))) STORED NULL,
	status varchar(50) DEFAULT 'SCHEDULED'::character varying NOT NULL,
	payment_method varchar(50) NULL,
	transaction_id varchar(100) NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	updated_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT payments_pkey PRIMARY KEY (payment_id),
	CONSTRAINT payments_loan_id_fkey FOREIGN KEY (loan_id) REFERENCES loans(loan_id)
);
