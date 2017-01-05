/** check if object is valid date */
export function isDate(obj: any): obj is Date {
    return obj instanceof Date && !isNaN(obj.valueOf());
}
