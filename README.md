# WebTrade

### Projects
WebTrade.Api - contains endpoints of the application and an exception handler.

WebTrade.Api.Test - contains integration tests.

WebTrade.Application - the layer where application logic is.

WebTrade.Domain - this project have the models from the application and expose some interfaces that are used by the application layer to get data from database.

WebTrade.Infrastructure - in this layer are placed database related things, migrations, repositories.

### Run application
- running the application, the swagger is opened and the database will be automatically created with some generated data.
- the swagger presents all the endpoints and requests could be send directly from swagger.
