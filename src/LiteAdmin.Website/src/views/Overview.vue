<template>
    <div class="home">
        <h1>{{getFriendlyName(tableName)}}</h1>

        <table>
            <thead>
                <tr>
                    <th v-for="(column, index) in tableSchema.columns"
                        v-if="index < 5 && !column.isPrimaryKey"
                        :key="column.name">
                        {{getFriendlyName(column.name)}}
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in items">
                    <td v-for="(column, index) in tableSchema.columns" 
                        v-if="index < 5 && !column.isPrimaryKey"
                        :key="column.name"
                        v-on:click="edit(item)">
                        {{item[column.name]}}
                    </td>

                </tr>
            </tbody>
        </table>
        <router-link :to="'/maintain/' + tableName + '/add'" class="button button--primary">Add</router-link>
    </div>
</template>

<script lang="ts" src="./Overview.ts"></script>
