import { Ticker } from '/js/Ticker.js';

function stopLoading() {
  document.querySelector('.loading-indicator').remove();
}

let ticker = new Ticker();

let symbols = new URLSearchParams(window.location.search).get('symbols');

ticker
  .loadData(symbols)
  .then(t => {
    t.runTicker();
  })
  .catch(error => {
    var errorDiv = document.createElement('div');
    errorDiv.textContent = error;
    errorDiv.style.color = 'red';

    document.body.append(errorDiv);

    stopLoading();
  });
