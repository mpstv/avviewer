import { TickerItem } from '/js/TickerItem.js';

export class Ticker {
  _tickerElement;

  _tickerItems = [];

  constructor() {
    this._tickerElement = document.querySelector('.ticker');
  }

  runTicker() {
    const container = document.querySelectorAll('.ticker-container');
    let speed = 1;

    container.forEach(function(el) {
      const tickerWrap = el.querySelector('.ticker-wrap');
      const ticker = el.querySelector('.ticker');

      const elWidth = ticker.offsetWidth;
      ticker.style.minWidth = `${elWidth}px`;

      let tickerClone = ticker.cloneNode(true);
      tickerWrap.appendChild(tickerClone);

      let progress = 1;
      function loop() {
        progress = progress - speed;

        if (progress <= elWidth * -1) {
          progress = 0;
        }

        tickerWrap.style.transform = 'translateX(' + progress + 'px)';

        window.requestAnimationFrame(loop);
      }
      loop();
    });
  }

  _createElement(header, changeValue, value) {
    let item = new TickerItem(this._tickerElement, header, changeValue, value);

    this._tickerItems.push(item);
  }

  loadData(symbols) {
    return new Promise(resolve => {
      fetch(`http://localhost:5000/symbols/?symbols=${symbols}`)
        .then(response => {
          response.json().then(json => {
            json.forEach(el => this._createElement(el.name, el.change, el.value));
            resolve(this);
          });
        })
        .catch(error => console.error(error));
    });
  }
}
