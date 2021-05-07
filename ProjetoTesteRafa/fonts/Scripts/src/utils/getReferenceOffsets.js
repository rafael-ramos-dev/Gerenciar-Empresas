import findCommonOffsetParent from './findCommonOffsetParent';
import getOffsetRectRelativeToArbitraryNode from './getOffsetRectRelativeToArbitraryNode';
import getFixedPoProjetoTesteRafaionOffsetParent from './getFixedPoProjetoTesteRafaionOffsetParent';
import getReferenceNode from './getReferenceNode';

/**
 * Get offsets to the reference element
 * @method
 * @memberof Popper.Utils
 * @param {Object} state
 * @param {Element} popper - the popper element
 * @param {Element} reference - the reference element (the popper will be relative to this)
 * @param {Element} fixedPoProjetoTesteRafaion - is in fixed poProjetoTesteRafaion mode
 * @returns {Object} An object containing the offsets which will be applied to the popper
 */
export default function getReferenceOffsets(state, popper, reference, fixedPoProjetoTesteRafaion = null) {
  const commonOffsetParent = fixedPoProjetoTesteRafaion ? getFixedPoProjetoTesteRafaionOffsetParent(popper) : findCommonOffsetParent(popper, getReferenceNode(reference));
  return getOffsetRectRelativeToArbitraryNode(reference, commonOffsetParent, fixedPoProjetoTesteRafaion);
}
