var map = null;
var pushpins = [];

function AddMapChildScriptToDocument() {
    if (!map) {
        var mapScriptUrl = "http://www.bing.com/api/maps/mapcontrol?onscriptload=LoadMapToPage";
        var script = document.createElement("script");
        script.setAttribute("defer", "");
        script.setAttribute("async", "");
        script.setAttribute("type", "text/javascript");
        script.setAttribute("src", mapScriptUrl);
        document.body.appendChild(script);
    }
}

function GetMap() {
    map = new Microsoft.Maps.Map(
        document.getElementById("theMap"),
        {
            credentials: "Ap_yaivH-quNP6hfiluV68u4icLC8_uUT-l4fS1TYABEXTlcm6UmzIHqSf1M8wMi",
            mapTypeId: Microsoft.Maps.MapTypeId.aerial,
            zoom: 0
        });
}

function SetMapCenterToAddress(pushpinTitle, address) {
    var args = { pushpinTitle: pushpinTitle, address: address };
    ExecuteWithViewLocationFromAddress(address,
        function (viewLocation, args) {
            RemovePushpins();
            AddPushpinToLocation(args.pushpinTitle, args.address, viewLocation.location);
            map.setView({ bounds: viewLocation.bestView });
        }, args);
}

function RemovePushpins() {
    map.entities.clear();
    pushpins = [];
}

function AddPushpinsForEventsNearAddress(address) {
    var args = {};
    ExecuteWithViewLocationFromAddress(
        address,
        function (viewLocation, args) {
            viewLocation.bestView.zoom = 0;
            map.setView({ bounds: viewLocation.bestView });
            var locationData = { latitude: viewLocation.location.latitude, longitude: viewLocation.location.longitude };
            var jsonLocationData = JSON.stringify(locationData);
            $.ajax({
                url: "/Search/SearchByLocation/",
                contentType: "application/json",
                type: "POST",
                data: jsonLocationData,
                success: AddPushpinsToJsonDinners,
                error: function (error) { alert(JSON.stringify(error)); }
            });
        },
        args
    );
}

function AddPushpinsToJsonDinners(dinners) {
    for (var i = 0; i < dinners.length; i++)
        AddPushpinToJsonDinner(i, dinners[i]);
}

function AddPushpinToJsonDinner(index, dinner) {
    var dinnerLocation = new Microsoft.Maps.Location(dinner.Latitude, dinner.Longitude);
    AddPushpinToLocationForDinner(dinner, dinnerLocation);
}

function ExecuteWithViewLocationFromAddress(address, toExecute, args) {
    Microsoft.Maps.loadModule('Microsoft.Maps.Search', function () {
        var searchManager = new Microsoft.Maps.Search.SearchManager(map);
        var requestOptions = {
            bounds: map.getBounds(),
            where: address,
            userArgs: args,
            callback: function (answer, userData) {
                var viewLocation = {};
                viewLocation.location = answer.results[0].location;
                viewLocation.bestView = answer.results[0].bestView;
                toExecute(viewLocation, this.userArgs);
            }
        };
        searchManager.geocode(requestOptions);
    });
}

function AddPushpinToLocation(pushpinTitle, address, location) {
    var pushpin = new Microsoft.Maps.Pushpin(location, { title: pushpinTitle, subTitle: address });
    map.entities.push(pushpin);
    pushpins.push(pushpin);
    return pushpin;
}

function AddPushpinToLocationForDinner(dinner, location) {
    var pushpin = new Microsoft.Maps.Pushpin(location, { title: dinner.Title, subTitle: dinner.Address });
    pushpin.dinner = dinner;
    AddDetailsRedirectActionForDinnerPushpinOnClick(pushpin);
    map.entities.push(pushpin);
    pushpins.push(pushpin);
    return pushpin;
}

function AddDetailsRedirectActionForDinnerPushpinOnClick(pushpin) {
    Microsoft.Maps.Events.addHandler(pushpin, 'click', RedirectToDetailsPushpinClickEventHandler);
}

function RedirectToDetailsPushpinClickEventHandler(event) {
    var pushpin = event.target;
    var url = "/Dinners/Details?id=__id__";
    window.location.href = url.replace('__id__', pushpin.dinner.DinnerId);
}