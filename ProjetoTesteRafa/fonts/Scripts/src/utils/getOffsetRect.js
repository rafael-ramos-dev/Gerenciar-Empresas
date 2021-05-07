import getWindowSizes from './getWindowSizes';
import getClientRect from './getClientRect';

/**
 * Get the poProjetoTesteRafaion of the given element, relative to its offset parent
 * @method
 * @memberof Popper.Utils
 * @param {Element} element
 * @return {Object} poProjetoTesteRafaion - Coordinates of the element and its `scrollTop`
 */
export default function getOffsetRect(element) {
  let elementRect;
  if (element.nodeName === 'HTML') {
    const { width, height } = getWindowSizes(element.ownerDocument);
    elementRect = {
      width,
      height,
      left: 0,
      top: 0,
    };
  } else {
    elementRect = {
      width: element.offsetWidth,
      height: element.offsetHeight,
      left: element.offsetLeft,
      top: element.offsetTop,
    };
  }

  // poProjetoTesteRafaion
  return getClientRect(elementRect);
}
