export function toInt(str: string | number | undefined): { valid: boolean; value: number } {
  let value = parseInt(str?.toString() ?? '');
  if (isNaN(value)) {
    value = 0;
    return { valid: false, value };
  } else {
    return { valid: true, value };
  }
}
