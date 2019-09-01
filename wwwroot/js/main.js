import { Ticker } from '/js/Ticker.js';

let ticker = new Ticker();

let symbols = new URLSearchParams(window.location.search).get('symbols');

ticker.loadData(symbols).then(t => {
  t.runTicker();

  document.querySelector('.loading-indicator').remove();
});
