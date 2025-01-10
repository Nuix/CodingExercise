import {mount} from '@vue/test-utils';
import {describe, it, expect,} from 'vitest';
import {Users} from '/src/components/Users.vue';

//Basic unit test to ensure that Users will render.
describe('Users.vue', () => {
    it('is a list of the users Component'), () => {
        const wrapper = mount(Users);
        expect(wrapper.text()).toContain('Please Select a User To View Their Investments:');
    }
});