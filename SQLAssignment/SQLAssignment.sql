CREATE DATABASE SQLAssignment;

use SQLAssignment;

CREATE table candidates (candidate_id VARCHAR(3),skill VARCHAR(10));

INSERT INTO candidates (candidate_id, skill)VALUES('001', 'C#'),('001', 'MVC'),('001', 'SQL'),('145', 'MVC'),('145', 'PowerBI'),('145', 'SQL'),('345', 'C#'),('345', 'MVC'),('200', 'SQL');


Select * from candidates;

--1.Given a table of candidates and their skills, you're tasked with finding the candidates best suited for an open .Net MVC developer job. You want to find candidates who are proficient in C#, MVC, and SQL.Write a query to list the candidates who possess all of the required skills for the job. Sort the output by candidate ID in ascending order.

SELECT candidate_id
FROM candidates
WHERE skill IN ('C#', 'MVC', 'SQL')
GROUP BY candidate_id
HAVING COUNT(DISTINCT skill) = 3
ORDER BY candidate_id ASC;

--2.	Assume you are given the tables below about Instagram pages and page likes. Write a query to return the page IDs of all the Instagram pages that don't have any likes. The output should be in ascending order.

CREATE TABLE pages (page_id INT PRIMARY KEY,page_name VARCHAR(255));

INSERT INTO pages (page_id, page_name)VALUES(20001, 'SQL Solutions'),(20045, 'Brain Exercises'),(20701, 'Tips for Data Analysts');

Select * from pages;

CREATE TABLE likes ( user_id INT, page_id INT, liked_date DATETIME,
  PRIMARY KEY (user_id, page_id),
  FOREIGN KEY (page_id) REFERENCES pages(page_id)
);

INSERT INTO likes (user_id, page_id, liked_date)
VALUES (111, 20001, '2022-04-08 00:00:00'),
       (121, 20045, '2022-03-12 00:00:00'),
       (156, 20001, '2022-07-25 00:00:00');
Select * from likes;

SELECT page_id
FROM pages
WHERE page_id NOT IN (SELECT page_id FROM likes)
ORDER BY page_id ASC;

--3. Write a query to get the number of companies that have posted duplicate job listings. Clarification: Duplicate job listings refer to two jobs at the same company with the same title and description.
CREATE TABLE job (job_id INT,company_id INT,title VARCHAR(50),description TEXT);

INSERT INTO job (job_id, company_id, title, description)
VALUES 
  (248, 827, 'Business Analyst', 'Business analyst evaluates past and current business data with the primary goal of improving decision-making processes within organizations.'),
  (149, 845, 'Business Analyst', 'Business analyst evaluates past and current business data with the primary goal of improving decision-making processes within organizations.'),
  (945, 345, 'Data Analyst', 'Data analyst reviews data to identify key insights into a business''s customers and ways the data can be used to solve problems.'),
  (164, 345, 'Data Analyst', 'Data analyst reviews data to identify key insights into a business''s customers and ways the data can be used to solve problems.'),
  (172, 244, 'Data Engineer', 'Data engineer works in a variety of settings to build systems that collect, manage, and convert raw data into usable information for data scientists and business analysts to interpret.');

  select * from job
--WAY 1
  SELECT COUNT(DISTINCT j1.company_id) AS num_duplicate_companies
FROM job j1
JOIN job j2 ON j1.company_id = j2.company_id
  AND j1.title = j2.title
  AND j1.job_id < j2.job_id


--Way 2
  SELECT COUNT(DISTINCT company_id) AS num_duplicate_companies
FROM (
  SELECT company_id, title
  FROM job
  GROUP BY company_id, title
  HAVING COUNT(*) > 1
) AS duplicates



--4.Assume you are given the table below on Uber transactions made by users. Write a query to obtain the third transaction of every user. Output the user id, spend and transaction date.

CREATE TABLE uber_transactions (user_id INT,spend FLOAT,transaction_date DATETIME);

INSERT INTO uber_transactions (user_id, spend, transaction_date)
VALUES (111, 100.50, '01/08/2022 12:00:00'),
	(111, 55.00, '01/10/2022 12:00:00'),
	(121, 36.00, '01/18/2022 12:00:00'),
	(145, 24.99, '01/26/2022 12:00:00'),
	(111, 89.60, '02/05/2022 12:00:00');

INSERT INTO uber_transactions (user_id, spend, transaction_date)
VALUES (111, 101.50, '01/08/2022 12:00:00');
	Select * from uber_transactions;

SELECT user_id, spend, transaction_date
FROM uber_transactions t1
WHERE (
  SELECT COUNT(*)
  FROM uber_transactions t2
  WHERE t2.user_id = t1.user_id AND t2.transaction_date < t1.transaction_date
) = 3;

--5.Assume there are three Spotify tables containing information about the artists, songs, and music charts. Write a query to determine the top 5 artists whose songs appear in the Top 10 of the global_song_rank table the highest number of times. From now on, we'll refer to this ranking number as "song appearances".Output the top 1 artist names with their song appearances ranking (not the number of song appearances, but the rank of who has the most appearances). The order of the rank should take precedence. For example, Ed Sheeran's songs appeared 5 times in Top 10 list of the global song rank table; this is the highest number of appearances, so he is ranked 1st. Bad Bunny's songs appeared in the list 4, so he comes in at a close 2nd
CREATE TABLE artist ( artist_id INT PRIMARY KEY, artist_name VARCHAR(50));

CREATE TABLE song (song_id INT PRIMARY KEY,artist_id INT,FOREIGN KEY (artist_id) REFERENCES artist(artist_id));

CREATE TABLE chart ( day INT, song_id INT, rank INT, FOREIGN KEY (song_id) REFERENCES song(song_id));



INSERT INTO artist (artist_id, artist_name)
VALUES (101, 'Ed Sheeran'), (120, 'Drake');

INSERT INTO song (song_id, artist_id)
VALUES (45202, 101), (19960, 120);

INSERT INTO chart (day, song_id, rank)
VALUES (1, 45202, 5), (3, 45202, 2), (1, 19960, 3), (9, 19960, 15);

select * from artist
select * from song
select * from chart





SELECT top 5
  a.artist_name,
  CASE 
    WHEN RANK() OVER (ORDER BY COUNT(*) DESC) = 1 THEN '1st'
    WHEN RANK() OVER (ORDER BY COUNT(*) DESC) = 2 THEN '2nd'
    WHEN RANK() OVER (ORDER BY COUNT(*) DESC) = 3 THEN '3rd'
    ELSE CONCAT(RANK() OVER (ORDER BY COUNT(*) DESC), 'th')
  END AS song_appearances_rank
FROM 
  artist a
JOIN 
  song s ON a.artist_id = s.artist_id
JOIN 
  chart g ON s.song_id = g.song_id
WHERE 
  g.rank <= 10
GROUP BY 
  a.artist_id, a.artist_name
ORDER BY 
  COUNT(*) DESC;


 


--6.	Assume you are given the table on Walmart user transactions. Based on a user's most recent transaction date, write a query to obtain the users and the number of products bought.Output the user's most recent transaction date, user ID and the number of products sorted by the transaction date in chronological order.
CREATE TABLE Walmart_user_transactions (product_id INT,user_id INT,Spend FLOAT,transaction_date DATETIME);

INSERT INTO Walmart_user_transactions VALUES
(3673, 123, 68.90, '2022-07-08 12:00:00'),
(9623, 123, 274.10, '2022-07-08 12:00:00'),
(1467, 115, 19.90, '2022-07-08 12:00:00'),
(2513, 159, 25.00, '2022-07-08 12:00:00'),
(1452, 159, 74.50, '2022-07-10 12:00:00');


select * from Walmart_user_transactions;

SELECT t.user_id, t.transaction_date AS most_recent_date, COUNT(t.product_id) AS num_products
FROM Walmart_user_transactions t
WHERE t.transaction_date = (SELECT MAX(transaction_date) FROM Walmart_user_transactions WHERE user_id = t.user_id)
GROUP BY t.user_id, t.transaction_date
ORDER BY most_recent_date ASC;


--7.You’re a consultant for a major pizza chain that will be running a promotion where all 3-topping pizzas will be sold for a fixed price, and are trying to understand the costs involved.Given a list of pizza toppings, consider all the possible 3-topping pizzas, and print out the total cost of those 3 toppings. Sort the results with the highest total cost on the top followed by pizza toppings in ascending order.Break ties by listing the ingredients in alphabetical order, starting from the first ingredient, followed by the second and third. 

CREATE TABLE pizza_toppings (topping_name VARCHAR(20),ingredient_cost DECIMAL(4,2));

INSERT INTO pizza_toppings (topping_name, ingredient_cost) VALUES
('Pepperoni', 0.50),
('Sausage', 0.70),
('Chicken', 0.55),
('Extra Cheese', 0.40);

SELECT CONCAT_WS(', ', t1.topping_name, t2.topping_name, t3.topping_name) AS toppings,
       ROUND((t1.ingredient_cost + t2.ingredient_cost + t3.ingredient_cost), 2) AS total_cost
FROM pizza_toppings AS t1
CROSS JOIN pizza_toppings AS t2
CROSS JOIN pizza_toppings AS t3
WHERE t1.topping_name < t2.topping_name AND t2.topping_name < t3.topping_name
ORDER BY total_cost DESC, toppings ASC;

---8.Assume you have the table below containing information on Facebook user actions. Write a query to obtain the active user retention in May 2022. Output the month (in numerical format 1, 2, 3) and the number of monthly active users (MAUs). Hint: An active user is a user who has user action ("sign-in", "like", or "comment") in the current month and last month.

CREATE TABLE user_events (
  user_id INT,
  event_id INT,
  event_type VARCHAR(20),
  event_date DATETIME
);

INSERT INTO user_events (user_id, event_id, event_type, event_date)
VALUES(445, 7765, 'sign-in', '2022-05-31 12:00:00'),
  (742, 6458, 'sign-in', '2022-06-03 12:00:00'),
  (445, 3634, 'like', '2022-06-05 12:00:00'),
  (742, 1374, 'comment', '2022-06-05 12:00:00'),
  (648, 3124, 'like', '2022-06-18 12:00:00');
  select * from user_events




SELECT
   
  COUNT(DISTINCT user_id) AS MAUs
FROM user_events
WHERE
  MONTH(event_date) IN (5, 6) AND YEAR(event_date) = 2022 AND event_type IN ('sign-in', 'like', 'comment')
GROUP BY user_id
HAVING COUNT(DISTINCT CASE WHEN MONTH(event_date) = 5 THEN 1 END) > 0
   AND COUNT(DISTINCT CASE WHEN MONTH(event_date) = 6 THEN 1 END) > 0;