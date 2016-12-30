import { Injectable } from '@angular/core';

@Injectable()
export class LocalStorageService {

   public constructor() {
        if (typeof window === 'undefined') {
            throw new Error('window is not defined!');
        }

        if (typeof window.localStorage === 'undefined') {
            throw new Error('window.localStorage is not defined!');
        }
   }

    public getItem(key: string): any {
        return window.localStorage.getItem(key);
    }

    public clear(): void {
        window.localStorage.clear();
    }

    public key(index: number): string {
        return window.localStorage.key(index);
    }

    public setItem(key: string, data: string): void {
        window.localStorage.setItem(key, data);
    }

    public get length(): number {
        return window.localStorage.length;
    }

    public removeItem(key: string): void {
        window.localStorage.removeItem(key);
    }
}
