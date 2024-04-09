const connection = new signalR.HubConnectionBuilder()
    .withUrl("/exchangeHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.start().then(function () {
    console.log("SignalR Connected.");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveExchangeRates", function (exchangeRates) {
    for (let currency in exchangeRates) {
        document.getElementById(currency).innerText = exchangeRates[currency];
    }
});