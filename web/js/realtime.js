function update() {

    var data;

    fetch('/latest')
        .then(function(resp) {
            return resp.json();
        })
        .then(function(response) {
            data = response;
            var table = document.querySelector('#latest').children;
            table[0].textContent = data.TimeStamp;
            table[1].textContent = data.Co;
            table[2].textContent = data.No;
            table[3].textContent = data.So;

        })
        .catch(function(error) {
            console.log(error);
        });




}

setInterval(update, 60000);
