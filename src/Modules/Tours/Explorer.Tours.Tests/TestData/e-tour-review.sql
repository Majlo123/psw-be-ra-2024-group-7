INSERT INTO tours."TourReview"(
"Id", "TourId", "Rating", "Comment", "TouristId", "VisitDate", "ReviewDate", "Images")
VALUES (-1, -1, 5, 'Amazing tour, highly recommended!', -21, '2023-05-15', '2023-06-01', ARRAY['img1.jpg', 'img2.jpg']);

INSERT INTO tours."TourReview"(
"Id", "TourId", "Rating", "Comment", "TouristId", "VisitDate", "ReviewDate", "Images")
VALUES (-2, -2, 4, 'Great experience, but a bit too long.', -22, '2023-07-12', '2023-07-20', ARRAY['img3.jpg']);

INSERT INTO tours."TourReview"(
"Id", "TourId", "Rating", "Comment", "TouristId", "VisitDate", "ReviewDate", "Images")
VALUES (-3, -3, 3, 'It was okay, but I expected more.', -23, '2023-08-05', '2023-08-12', ARRAY[]::text[]);