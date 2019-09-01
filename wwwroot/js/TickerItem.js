export class TickerItem {
  constructor(tickerElement, header, changeValue, value) {
    let item = tickerElement.appendChild(this._createTickerItemElement());

    this._createHeaderElement(item, header);
    this._createChangeElement(item, changeValue);
  
    this._createValueElement(item, value);  
  }

  _createValueElement(item, value) {
    let valueElement = document.createElement('div');
    
    valueElement.classList.add('value');
    valueElement.innerText = value;

    item.appendChild(valueElement);
  }

  _createChangeElement(item, changeValue) {
    let changeElement = document.createElement('div');
    changeElement.classList.add('change');
    
    if (changeValue >= 0) {
      changeElement.classList.add('up');
    }
    else {
      changeElement.classList.add('down');
    }
    
    changeElement.innerText = Math.abs(changeValue);
    
    item.appendChild(changeElement);
  }

  _createHeaderElement(item, header) {
    let headerElement = document.createElement('div');
    
    headerElement.classList.add('ticker-header');
    headerElement.innerText = header;
    
    item.appendChild(headerElement);
  }

  _createTickerItemElement() {
    let item = document.createElement('div');
    item.className = 'ticker-item';
    return item;
  }
}