# vintri exercise

BeerController is the controller used for developing endpoints
There are 2 http methods in the controller
    GetById(int id) is used to query the punk api and also to store the results in database.json file.
    PutBeerRatings(int id, BeerUserRatings data) is used to query the punk api if the beer with id is not already written to database.json file and also updates the data with user     ratings
    There are unit tests for both the methods in Exercises.Test project
