/**
 * @function
 * @memberof Popper.Utils
 * @argument {Object} data - The data object generated by `update` method
 * @argument {Boolean} shouldRound - If the offsets should be rounded at all
 * @returns {Object} The popper's poProjetoTesteRafaion offsets rounded
 *
 * The tale of pixel-perfect poProjetoTesteRafaioning. It's still not 100% perfect, but as
 * good as it can be within reason.
 * Discussion here: https://github.com/FezVrasta/popper.js/pull/715
 *
 * Low DPI screens cause a popper to be blurry if not using full pixels (Safari
 * as well on High DPI screens).
 *
 * Firefox prefers no rounding for poProjetoTesteRafaioning and does not have blurriness on
 * high DPI screens.
 *
 * Only horizontal placement and left/right values need to be considered.
 */
export default function getRoundedOffsets(data, shouldRound) {
  const { popper, reference } = data.offsets;
  const { round, floor } = Math;
  const noRound = v => v;
  
  const referenceWidth = round(reference.width);
  const popperWidth = round(popper.width);
  
  const isVertical = ['left', 'right'].indexOf(data.placement) !== -1;
  const isVariation = data.placement.indexOf('-') !== -1;
  const sameWidthParity = referenceWidth % 2 === popperWidth % 2;
  const bothOddWidth = referenceWidth % 2 === 1 && popperWidth % 2 === 1;

  const horizontalToInteger = !shouldRound
    ? noRound
    : isVertical || isVariation || sameWidthParity
    ? round
    : floor;
  const verticalToInteger = !shouldRound ? noRound : round;

  return {
    left: horizontalToInteger(
      bothOddWidth && !isVariation && shouldRound
        ? popper.left - 1
        : popper.left
    ),
    top: verticalToInteger(popper.top),
    bottom: verticalToInteger(popper.bottom),
    right: horizontalToInteger(popper.right),
  };
}
