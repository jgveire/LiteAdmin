<template>
    <div class="home">
        <md-table>
            <md-table-toolbar>
                <h1 class="md-title">{{getFriendlyName(tableName)}} Overview</h1>
            </md-table-toolbar>
            <md-table-row>
                <md-table-head v-for="(column, index) in tableSchema.columns"
                               v-if="index < 5 && !column.isPrimaryKey">
                    {{getFriendlyName(column.name)}}
                </md-table-head>
            </md-table-row>
            <md-table-row v-for="item in items" 
                          v-on:click="edit(item)">
                <md-table-cell v-for="(column, index) in tableSchema.columns"
                               v-if="index < 5 && !column.isPrimaryKey">
                    {{item[column.name]}}
                </md-table-cell>

            </md-table-row>
        </md-table>
        <md-button class="md-fab md-fab-bottom-right" :to="'/maintain/' + tableName + '/add'" >
            <md-icon>add</md-icon>
        </md-button>
    </div>
</template>

<script lang="ts" src="./Overview.ts"></script>
