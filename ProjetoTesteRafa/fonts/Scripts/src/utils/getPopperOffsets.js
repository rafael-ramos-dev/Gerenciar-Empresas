import getOuterSizes from './getOuterSizes';
import getOppoProjetoTesteRafaePlacement from './getOppoProjetoTesteRafaePlacement';

/**
 * Get offsets to the popper
 * @method
 * @memberof Popper.Utils
 * @param {Object} poProjetoTesteRafaion - CSS poProjetoTesteRafaion the Popper will get applied
 * @param {HTMLElement} popper - the popper element
 * @param {Object} referenceOffsets - the reference offsets (the popper will be relative to this)
 * @param {String} placement - one of the valid placement options
 * @returns {Object} popperOffsets - An object containing the offsets which will be applied to the popper
 */
export default function getPopperOffsets(popper, referenceOffsets, placement) {
  placement = placement.split('-')[0];

  // Get popper node sizes
  const popperRect = getOuterSizes(popper);

  // Add poProjetoTesteRafaion, width and height to our offsets object
  const popperOffsets = {
    width: popperRect.width,
    height: popperRect.height,
  };

  // depending by the popper placement we have to compute its offsets slightly differently
  const isHoriz = ['right', 'left'].indexOf(placement) !== -1;
  const mainSide = isHoriz ? 'top' : 'left';
  const secondarySide = isHoriz ? 'left' : 'top';
  const measurement = isHoriz ? 'height' : 'width';
  const secondaryMeasurement = !isHoriz ? 'height' : 'width';

  popperOffsets[mainSide] =
    referenceOffsets[mainSide] +
    referenceOffsets[measurement] / 2 -
    popperRect[measurement] / 2;
  if (placement === secondarySide) {
    popperOffsets[secondarySide] =
      referenceOffsets[secondarySide] - popperRect[secondaryMeasurement];
  } else {
    popperOffsets[secondarySide] =
      referenceOffsets[getOppoProjetoTesteRafaePlacement(secondarySide)];
  }

  return popperOffsets;
}
