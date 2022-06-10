import http from 'k6/http';
import { check, group, sleep } from 'k6';

export const options = {
    scenarios: {
        stress: {
            executor: 'ramping-arrival-rate',
            preallocatedVUs: 400,
            exec: 'stress',
            stages: [

                { target: 100, duration: '1m' },
                { target: 100, duration: '2m' },
                { target: 200, duration: '2m' },
                { target: 200, duration: '2m' },
                { target: 400, duration: '2m' },
                { target: 400, duration: '2m' },
                { target: 0, duration: '5m' }
            ]
        },
        load: {
            executor: 'ramping-vus',
            exec: 'stress',
            gracefulRampDown: "5s",
            stages: [
                { target: 200, duration: '2m' },
                { target: 200, duration: '2m' },
                { target: 0, duration: '2m' }
            ]
        }
    }
};


export function stress() {
    http.get('https://localhost:44386/api/v1/Resources/All', {
        tags: { my_tag: 'stress' }
    });
}

export function load() {
    http.get('https://localhost:44386/api/v1/Resources/All', {
        tags: { my_tag: 'load' }
    });
}